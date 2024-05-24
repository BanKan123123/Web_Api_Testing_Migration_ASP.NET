namespace Web_Api_Testing_Migration.Models
{
    public class suggestbook
    {
        public int id { get; set; }
        public book book { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
