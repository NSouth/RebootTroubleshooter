using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RebootTroubleshooter.ViewModels
{
    public class RebootSummaryViewModel : Screen
    {
        private string _sampleText = "Loading...";
        public string SampleText { get => _sampleText; set { _sampleText = value; NotifyOfPropertyChange(); } }

        private bool _areEventsLoading = true;

        public bool AreEventsLoading
        {
            get => _areEventsLoading;
            set => Set(ref _areEventsLoading, value);
        }

        public BindableCollection<RebootEventSummaryViewModel> RebootEvents { get; set; } = new BindableCollection<RebootEventSummaryViewModel>();

        public RebootSummaryViewModel()
        {
            LoadRebootEvents();
        }

        public async void LoadRebootEvents()
        {
            SampleText = "Loading...";
            AreEventsLoading = true;
            await Task.Run(() =>
            {
                RebootEvents.Clear();
                RebootEvents.AddRange(GetRecentRebootEvents());
            });
            SampleText = "Loaded";
            AreEventsLoading = false;
        }

        private static IList<RebootEventSummaryViewModel> GetRecentRebootEvents()
        {
            var logName = "System";
            var eventLog = new EventLog(logName, ".", "System");


            // Load known reboot causes from JSON file
            var knownRebootCauses = LoadKnownRebootCauses("known_reboot_causes.json");

            // Define instance IDs for readability
            const int USER_INITIATED_SHUTDOWN_EVENT_ID = 1074;
            const int CLEAN_SHUTDOWN_EVENT_ID = 1076;
            const int NON_USER_INITIATED_SHUTDOWN_EVENT_ID = 6008;
            const int NON_CLEAN_SHUTDOWN_EVENT_ID = 6009;
            const int BSOD_EVENT_ID = 41; // BSOD or other critical failure

            // Get all relevant events from the past 7 days
            var events = eventLog.Entries
                .Cast<EventLogEntry>()
                .Where(x =>
                    (x.InstanceId == USER_INITIATED_SHUTDOWN_EVENT_ID ||
                     x.InstanceId == CLEAN_SHUTDOWN_EVENT_ID ||
                     x.InstanceId == NON_USER_INITIATED_SHUTDOWN_EVENT_ID ||
                     x.InstanceId == NON_CLEAN_SHUTDOWN_EVENT_ID ||
                     (x.InstanceId == BSOD_EVENT_ID && x.Source == "Microsoft-Windows-Kernel-Power")) && 
                    x.TimeGenerated >= DateTime.Now.AddDays(-180))
                .OrderByDescending(x => x.TimeGenerated)
                .Take(10);

            // Map events to reboot event summaries
            var rebootEvents = events
                .Select(x =>
                {
                    // Get the known reboot cause if it exists
                    var knownCause = knownRebootCauses
                        .FirstOrDefault(c => c.Codes.Contains(x.InstanceId));
                    var description = knownCause != null
                        ? knownCause.PlainEnglishDescription
                        : "Your computer restarted unexpectedly due to an unknown reason.";
                    var suggestion = knownCause != null
                        ? knownCause.SuggestionToPrevent
                        : "Check for and remove malware or viruses, ensure your computer is not overheating, and check your hardware for any failures or errors.";

                    // Create the summary object
                    return new RebootEventSummaryViewModel
                    {
                        DateTime = x.TimeGenerated,
                        InstanceId = x.InstanceId,
                        Level = x.EntryType == 0 ? "Unknown" : x.EntryType.ToString(),
                        Message = x.Message,
                        Source = x.Source,
                        User = x.UserName,
                        PlainEnglishDescription = description,
                        SuggestionToPrevent = suggestion
                    };
                });

            return rebootEvents.ToList();
        }


        public class RebootCause
        {
            public List<long> Codes { get; set; } = new List<long>();   
            public string PlainEnglishDescription { get; set; } = string.Empty;
            public string SuggestionToPrevent { get; set; } = string.Empty;
        }

        private static List<RebootCause> LoadKnownRebootCauses(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<RebootCause>>(json) ?? new List<RebootCause>();
        }
    }
}
