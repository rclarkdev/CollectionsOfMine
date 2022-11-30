namespace CollectionsOfMine.Data.Models
{
    public class Template : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Area> Areas { get; set; }
        public ICollection<Collection> Collections { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
