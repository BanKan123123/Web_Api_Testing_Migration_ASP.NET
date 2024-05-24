namespace Web_Api_Testing_Migration.Models
{
    public class BookChapter
    {
        public book book { get; set; }
        public ICollection<chapter> chapters { get; set; }
    }
}
