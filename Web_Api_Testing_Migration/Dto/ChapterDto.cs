
namespace Web_Api_Testing_Migration.Dto
{
    public class ChapterDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string data { get; set; }
        public BookDto book { get; set; }
        public int chapterIndex { get; set; }
        public string audioUrl { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool hiddenAds { get; set; }
        public string? summary { get; set; }
    }
}
