namespace CollectionsOfMine.Data.Models
{
    public class Collection : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long FileId { get; set; }
        public Area Area { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<ContentType> ContentTypes { get; set; }
    }
}
