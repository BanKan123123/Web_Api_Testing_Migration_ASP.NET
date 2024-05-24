using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Repositories.Implements
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(author model)
        {
            _context.Add(model);
            return saveChange();
        }
         
        public bool deleteAll()
        {
            throw new NotImplementedException();
        }

        public bool deleteOneBySlug(author model)
        {
            _context.Remove(model); 
            return saveChange();
        }

        public ICollection<author> findAll()
        {
            return _context.author.OrderBy(a => a.id).ToList();
        }

        public author findOneById(int id)
        {
            return _context.author.Where(a => a.id == id).FirstOrDefault();
        }

        public author findOneBySlug(string slug)
        {
            return _context.author.Where(a => a.slug == slug).FirstOrDefault();
        }

        public bool isExistById(int id)
        {
            return _context.author.Any(author => author.id == id);
        }

        public bool isExistBySlug(string slug)
        {
            return _context.author.Any(author => author.slug == slug);
        }
        public bool saveChange()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool updateBySlug(author model)
        {
            _context.Update(model);
            return saveChange();
        }
    }
}
