namespace CollectionsOfMine.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        public string Type { get; set; }
        public string Base64 { get; set; }
        public byte[] Bytes { get; set; }
    }
}
