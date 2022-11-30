namespace CollectionsOfMine.Data.Models
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string HtmlContent { get; set; }
        public string IframeSrc { get; set; }
        public DateTime? TimelineDate { get; set; }
        public ICollection<File> Files { get; set; }
        public Collection Collection { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Area> Areas { get; set; }
        public ICollection<Template> Templates { get; set; }
    }
}
