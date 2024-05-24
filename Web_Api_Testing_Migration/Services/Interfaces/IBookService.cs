using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Services.Interfaces
{
    public interface IBookService
    {
        void addBook(book book);
        List<book> getBooks();  
        void deleteBook(int id);    
        void updateBook(int id, book book);
        void deleteAllBooks();
        book getBookById(int id);   
    }
}
