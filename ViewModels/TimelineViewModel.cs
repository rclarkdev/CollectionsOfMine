namespace CollectionsOfMine.ViewModels
{
    public class TimelineViewModel : ViewModelBase
    {
        public ICollection<EventViewModel> TimelineEvents { get; set; }
    }
}
