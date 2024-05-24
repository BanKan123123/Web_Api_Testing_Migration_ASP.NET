namespace Web_Api_Testing_Migration.Dto
{
    public class BookChapterDto
    {
        public BookDto book;
        public ICollection<ChapterDto> chapters;
    }
}
