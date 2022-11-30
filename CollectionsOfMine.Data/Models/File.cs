namespace CollectionsOfMine.Data.Models
{
    public class File : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Base64 { get; set; }
        public byte[] Bytes { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
