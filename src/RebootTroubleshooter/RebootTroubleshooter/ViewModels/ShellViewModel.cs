using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace RebootTroubleshooter.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await ShowRebootSummary();
        }

        public Task ShowRebootSummary()
        {
            var viewmodel = IoC.Get<RebootSummaryViewModel>();
            return ActivateItemAsync(viewmodel, new CancellationToken());
        }
    }
}
