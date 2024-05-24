using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Services.Interfaces
{
    public interface ICategoryService
    {
        void addCategory(category category);
        List<category> getCategories();

        void deleteCategory(int id);
        void updateCategory(int id, category category);
        category getCategory(int id);
        category getCategoryBySlug(string slug);    
        void deleteAllCategories();
    }
}
