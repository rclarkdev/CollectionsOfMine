namespace CollectionsOfMine.Data.Models
{
    public class Area : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Collection> Collections { get; set; }

    }
}
