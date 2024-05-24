namespace Web_Api_Testing_Migration.Models
{
    public class categoriesonbook
    {
        public int bookId { get; set; }
        public book book { get; set; }
        public int categoryId { get; set; }
        public category category { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set;}
    }
}
