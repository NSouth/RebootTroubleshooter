using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebootTroubleshooter.ViewModels
{
    public class RebootEventSummaryViewModel : ViewAware
    {
        public DateTime DateTime { get; set; }
        public long InstanceId { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string PlainEnglishDescription { get; set; } = string.Empty;
        public string SuggestionToPrevent { get; set; } = string.Empty;


        private bool _isDescriptionExpanded;
        public bool IsDescriptionExpanded
        {
            get => _isDescriptionExpanded;
            set
            {
                if (value != _isDescriptionExpanded)
                {
                    _isDescriptionExpanded = value;
                    NotifyOfPropertyChange(nameof(IsDescriptionExpanded));
                    NotifyOfPropertyChange(nameof(DescriptionMaxLines));
                }
            }
        }

        public int DescriptionMaxLines => IsDescriptionExpanded ? int.MaxValue : 3;

        private bool _isDetailsPopupOpen;
        public bool IsDetailsPopupOpen
        {
            get { return _isDetailsPopupOpen; }
            set
            {
                _isDetailsPopupOpen = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _isSuggestionExpanded;

        public bool IsSuggestionExpanded
        {
            get { return _isSuggestionExpanded; }
            set
            {
                _isSuggestionExpanded = value;
                NotifyOfPropertyChange();
            }
        }
        public void ShowMore()
        {
            IsDescriptionExpanded = true;
            IsSuggestionExpanded = true;
        }

        public void ShowRawDetails()
        {
            IsDetailsPopupOpen = !IsDetailsPopupOpen;
        }

        public void ClosePopup() => IsDetailsPopupOpen = false;
    }
}
