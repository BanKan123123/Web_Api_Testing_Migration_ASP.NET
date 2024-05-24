namespace Web_Api_Testing_Migration.Models
{
    public class category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public ICollection<categoriesonbook> books { get; set; }
    }
}
