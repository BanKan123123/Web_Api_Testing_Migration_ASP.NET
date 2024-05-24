using Microsoft.EntityFrameworkCore;
using System.Net;
using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(category model)
        {
            _context.Add(model);
            return saveChange();
        }

        public bool CreateCategoriesOnBook(List<categoriesonbook> categoriesOnBooks)
        {
            _context.categoriesonbooks.AddRange(categoriesOnBooks);
            return _context.SaveChanges() > 0;
        }

        public bool deleteAll()
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategoriesOnBook(ICollection<categoriesonbook> categoriesonbooks)
        {
            _context.categoriesonbooks.RemoveRange(categoriesonbooks);
            return _context.SaveChanges() > 0;
        }

        public bool deleteOneBySlug(category model)
        {
            throw new NotImplementedException();
        }

        public ICollection<category> findAll()
        {
            return _context.category.OrderBy(c => c.id).ToList();
        }

        public category findOneById(int id)
        {
            return _context.category.Where(c => c.id == id).FirstOrDefault();
        }

        public category findOneBySlug(string slug)
        {
            return _context.category.Where(c => c.slug == slug).FirstOrDefault();
        }

        public ICollection<book> getBookByCategory(int id)
        {
            return _context.categoriesonbooks
                   .Where(cob => cob.categoryId == id)
                   .Include(cob => cob.book.author)
                   .Include(cob => cob.book.categories)
                   .ThenInclude(cob => cob.category)
                   .Select(cob => cob.book)
                   .ToList();
        }

        public bool isExistById(int id)
        {
            return _context.category.Any(c => c.id == id);
        }

        public bool isExistBySlug(string slug)
        {
            return _context.category.Any(c => c.slug == slug);
        }
        public bool saveChange()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public bool updateBySlug(category model)
        {
            throw new NotImplementedException();
        }
    }
}
