namespace CollectionsOfMine.Data.Models
{
    public class Attribute : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ValuesJson { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
