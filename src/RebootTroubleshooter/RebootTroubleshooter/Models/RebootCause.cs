namespace RebootTroubleshooter.Models
{
    public class RebootCause
    {
        public EventInfo EventInfo { get; set; } = new EventInfo();
        public string PlainEnglishDescription { get; set; } = string.Empty;
        public string SuggestionToPrevent { get; set; } = string.Empty;
    }
}
