using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Repositories.Interfaces
{
    public interface IBookRepository: IAbstractRepository<book>
    {
       ICollection<chapter> getChaptersById(int id);
       int createBook(book book);
    }
}
