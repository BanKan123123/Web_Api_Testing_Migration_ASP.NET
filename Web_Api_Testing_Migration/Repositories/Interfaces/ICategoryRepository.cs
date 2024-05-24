using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Repositories.Interfaces
{
    public interface ICategoryRepository : IAbstractRepository<category>
    {
        ICollection<book> getBookByCategory(int id);
        public bool CreateCategoriesOnBook(List<categoriesonbook> categoriesOnBooks);

        public bool DeleteCategoriesOnBook(ICollection<categoriesonbook> categoriesonbooks);
    }
}
