namespace Web_Api_Testing_Migration.Models
{
    public class book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string imageThumbnail { get; set; }
        public double rate { get; set; }
        public double view { get; set; }
        public double? realView { get; set; }
        public double like { get; set; }
        public double disable { get; set; }
        public double hiddenAds { get; set; }
        public string status { get; set; }
        public author author { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public ICollection<categoriesonbook> categories { get; set; }
    }
}
