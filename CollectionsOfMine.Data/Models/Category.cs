namespace CollectionsOfMine.Data.Models
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
