namespace CollectionsOfMine.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        public string Source { get; set; }
        public string IframeSrc { get; set; }
        public string FriendlyCreatedOn { get; set; }
        public string HtmlContent { get; set; }
        public long SelectedCollection { get; set; }
        public long SelectedArea { get; set; }
        public virtual ICollection<AreaViewModel> Areas { get; set; }
        public virtual CollectionViewModel Collection { get; set; }
        public virtual ICollection<FileViewModel> Files { get; set; }
        public virtual ICollection<AttributeViewModel> SelectedAttributes { get; set; }
    }
}
