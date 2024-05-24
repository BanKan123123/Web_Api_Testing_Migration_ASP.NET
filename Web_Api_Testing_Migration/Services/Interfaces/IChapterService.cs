using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Services.Interfaces
{
    public interface IChapterService
    {
        void addChapter(chapter chapter);
        List<chapter> GetChapters();
        void deleteChapter(int id);
        void updateChapter(int id, int chapter);
        void deleteAllChapters();
        chapter getChapterById(int id);
        chapter getChapterBySlug(string slug);
    }
}
