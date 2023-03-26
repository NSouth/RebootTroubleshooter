using Caliburn.Micro;
using RebootTroubleshooter.Services;
using System.Threading.Tasks;
using System.Windows;

namespace RebootTroubleshooter.ViewModels
{
    public class RebootSummaryViewModel : Screen
    {
        private bool _areEventsLoading = true;
        public bool AreEventsLoading
        {
            get => _areEventsLoading;
            set => Set(ref _areEventsLoading, value);
        }

        public BindableCollection<RebootEventViewModel> RebootEvents { get; set; } = new BindableCollection<RebootEventViewModel>();

        public RebootSummaryViewModel()
        {
            LoadRebootEvents();
        }

        public async void LoadRebootEvents()
        {
            AreEventsLoading = true;
            await Task.Run(() =>
            {
                RebootEvents.Clear();
                RebootEvents.AddRange(EventLogService.GetRecentRebootEvents());
            });
            AreEventsLoading = false;
        }

        public void ShowInfo()
        {
            MessageBox.Show("Hi! This app is simple. It finds recent reboot events in the system event log and shows them to you. It also attempts to make some events easier to understand and provides suggestions for how to address. Enjoy!", "Info");
        }        
    }
}
