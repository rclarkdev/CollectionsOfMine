namespace CollectionsOfMine.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        public DateTime EventDate { get; set; }
        public ICollection<ItemViewModel> EventItems { get; set; }
    }
}
