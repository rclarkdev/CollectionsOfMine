namespace CollectionsOfMine.ViewModels
{
    public class CollectionViewModel : ViewModelBase
    {
        public string FriendlyCreatedOn { get; set; }
        public long SelectedArea { get; set; }
        public long FileId { get; set; }
        public AreaViewModel Area { get; set; }
        public ICollection<ItemViewModel> Items { get; set; }
        public ICollection<ContentTypeViewModel> ContentTypes { get; set; }
    }
}
