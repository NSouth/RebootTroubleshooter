using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebootTroubleshooter.ViewModels
{
    public class RebootSummaryViewModel : Screen
    {
        private string _sampleText = "Bob";
        public string SampleText { get => _sampleText; set { _sampleText = value; NotifyOfPropertyChange(); } }

        public void LoadRebootEvents()
        {
            SampleText = "Foo";
        }
    }
}
