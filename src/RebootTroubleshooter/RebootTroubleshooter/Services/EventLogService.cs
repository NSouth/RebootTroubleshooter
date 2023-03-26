using RebootTroubleshooter.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace RebootTroubleshooter.Services
{
    internal class EventLogService
    {
        public static IList<RebootEventViewModel> GetRecentRebootEvents()
        {
            var logName = "System";
            var eventLog = new EventLog(logName, ".", "System");

            // Load known reboot causes from JSON file
            var knownRebootCauses = RebootCauseService.LoadKnownRebootCauses("known_reboot_causes.json");

            var events = eventLog.Entries
                .Cast<EventLogEntry>()
                .Where(x =>
                /* 
                 * By performing a bitwise AND operation with the event's instance ID and the bitmask 0x3FFFFFFF, 
                 * you are ignoring the most significant bit, which is used to indicate whether the event is a success or failure audit.
                 * https://stackoverflow.com/a/47859949
                 */
                    knownRebootCauses.Select(c => c.EventInfo.EventId).Contains((x.InstanceId & 0x3FFFFFFF)) &&
                    x.TimeGenerated >= DateTime.Now.AddDays(-180))
                .OrderByDescending(x => x.TimeGenerated)
                .Take(10);



            // Map events to reboot event summaries
            var rebootEvents = events
                .Select(x =>
                {
                    // Get the known reboot cause if it exists
                    var instanceIdWithoutAuditBit = (x.InstanceId & 0x3FFFFFFF);
                    var knownCause = knownRebootCauses
                        .FirstOrDefault(c => c.EventInfo.EventId == instanceIdWithoutAuditBit);
                    var description = knownCause?.PlainEnglishDescription ?? "Your computer restarted unexpectedly due to an unknown reason.";
                    var suggestion = knownCause?.SuggestionToPrevent ?? "Check for and remove malware or viruses, ensure your computer is not overheating, and check your hardware for any failures or errors.";

                    // Create the summary object
                    var result = new RebootEventViewModel
                    {
                        DateTime = x.TimeGenerated,
                        InstanceId = instanceIdWithoutAuditBit,
                        EventCodeHumanized = knownCause?.EventInfo?.EventCodeHumanized ?? "[Unknown]",
                        Level = x.EntryType == 0 ? "[Unknown]" : x.EntryType.ToString(),
                        Message = x.Message,
                        Source = x.Source,
                        User = string.IsNullOrWhiteSpace(x.UserName) ? "[Unknown]" : x.UserName,
                        PlainEnglishDescription = description,
                        SuggestionToPrevent = suggestion
                    };

                    if (instanceIdWithoutAuditBit == 1074) // User-initiated events have more info we can extract
                    {
                        try
                        {
                            var message = x.Message;
                            // Parse out the reason, reason code, and shutdown type from the message using regular expressions
                            Match reasonMatch = Regex.Match(message, @"following reason: (?<reason>.+)\r\n");
                            Match reasonCodeMatch = Regex.Match(message, @"Reason Code: (?<reasonCode>0x[0-9a-fA-F]+)\r\n");
                            Match shutdownTypeMatch = Regex.Match(message, @"Shutdown Type: (?<shutdownType>.+)\r\n");

                            // Extract the values from the matches
                            string reason = reasonMatch.Groups["reason"].Value.Trim();
                            string reasonCode = reasonCodeMatch.Groups["reasonCode"].Value.Trim();
                            string shutdownType = shutdownTypeMatch.Groups["shutdownType"].Value.Trim();

                            result.MessageReason = reason;
                            result.MessageReasonCode = reasonCode;
                            result.MessageShutdownType = shutdownType;
                            if (Models.EventReasonCodes.Dictionary.TryGetValue(reasonCode, out var value))
                            {
                                result.MessageReasonCodeHumanReadable = value;
                            };
                        }
                        catch { }
                    }

                    return result;
                });

            return rebootEvents.ToList();
        }
    }
}
