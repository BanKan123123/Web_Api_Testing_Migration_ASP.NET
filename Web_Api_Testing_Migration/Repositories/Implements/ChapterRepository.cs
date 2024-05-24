using Microsoft.EntityFrameworkCore;
using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Repositories.Implements
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly DataContext _context;

        public ChapterRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(chapter model)
        {
            _context.Add(model);
            return saveChange();
        }

        public bool deleteAll()
        {
            throw new NotImplementedException();
        }

        public bool deleteOneBySlug(chapter model)
        {
            _context.Remove(model);
            return saveChange();
        }

        public ICollection<chapter> findAll()
        {
            return _context.chapter.Include(c => c.book).Include(c => c.book.author).Include(c => c.book.categories).ThenInclude(cob => cob.category).OrderBy(c => c.book.title).ToList();
        }
        public bool isExistById(int id)
        {
            return _context.chapter.Any(c => c.id == id);
        }

        public bool isExistBySlug(string slug)
        {
            return _context.chapter.Any(c => c.slug == slug);
        }

        public bool saveChange()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool updateBySlug(chapter model)
        {
            _context.Update(model);
            return saveChange();
        }
        chapter IAbstractRepository<chapter>.findOneById(int id)
        {
            return _context.chapter.Where(c => c.id == id).Include(c => c.book).Include(c => c.book.author).Include(c => c.book.categories).ThenInclude(cob => cob.category).FirstOrDefault();
        }

        chapter IAbstractRepository<chapter>.findOneBySlug(string slug)
        {
            return _context.chapter.Where(c => c.slug == slug).Include(c => c.book).Include(c => c.book.author).Include(c => c.book.categories).ThenInclude(cob => cob.category).FirstOrDefault();
        }
    }
}
