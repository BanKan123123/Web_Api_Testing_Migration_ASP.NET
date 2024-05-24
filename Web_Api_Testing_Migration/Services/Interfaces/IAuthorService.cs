using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Services.Interfaces
{
    public interface IAuthorService
    {
        void addAuthor(author author);

        List<author> GetAuthors();

        void deleteAuthor(int id);

        void updateAuthor(int id, author author);

        void deleteAllAuthors();

        book getAuthorById(int id);
    }
}
