using Microsoft.EntityFrameworkCore;
using Web_Api_Testing_Migration.Data;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Repositories.Implements
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(book model)
        {
            throw new NotImplementedException();
        }

        public int createBook(book book)
        {
            book bookCreate = new book
            {
                title = book.title,
                slug = book.slug,
                description = book.description,
                imageThumbnail = book.imageThumbnail,
                rate = book.rate,
                view = book.view,
                realView = book.realView,
                like = book.like,
                disable = book.disable,
                hiddenAds = book.hiddenAds,
                status = book.status,
                author = book.author,
                created_at = book.created_at,
                updated_at = book.updated_at,
            };
            _context.book.Add(bookCreate);
            _context.SaveChanges();
            return bookCreate.id;
        }

        public bool deleteAll()
        {
            throw new NotImplementedException();
        }

        public bool deleteOneBySlug(book model)
        {
            _context.Remove(model);
            return saveChange();
        }

        public ICollection<book> findAll()
        {
            return _context.book.Include(book => book.author).Include(book => book.categories).ThenInclude(cob => cob.category).ToList();
        }

        public book findOneById(int id)
        {
            return _context.book.Where(b => b.id == id).Include(book => book.author).Include(book => book.categories).ThenInclude(cob => cob.category).FirstOrDefault();
        }

        public book findOneBySlug(string slug)
        {
            return _context.book.Where(b => b.slug == slug).Include(book => book.author).Include(book => book.categories).ThenInclude(cob => cob.category).FirstOrDefault();
        }

        public ICollection<chapter> getChaptersById(int id)
        {
            return _context.chapter.Where(c => c.book.id == id).Include(c => c.book).Include(c => c.book.author).Include(c => c.book.categories).ThenInclude(cob => cob.category).ToList();
        }

        public bool isExistById(int id)
        {
            return _context.book.Any(b => b.id == id);
        }

        public bool isExistBySlug(string slug)
        {
            return _context.book.Any(b => b.slug == slug);
        }

        public bool saveChange()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public bool updateBySlug(book model)
        {
            // Lấy bản ghi hiện có từ cơ sở dữ liệu
            var bookUpdate = _context.book.FirstOrDefault(b => b.slug == model.slug);

            if (bookUpdate == null)
            {
                return false;
            }

            bookUpdate.title = model.title;
            bookUpdate.description = model.description;
            bookUpdate.imageThumbnail = model.imageThumbnail;
            bookUpdate.rate = model.rate;
            bookUpdate.view = model.view;
            bookUpdate.realView = model.realView;
            bookUpdate.like = model.like;
            bookUpdate.disable = model.disable;
            bookUpdate.hiddenAds = model.hiddenAds;
            bookUpdate.status = model.status;
            bookUpdate.author = model.author;
            bookUpdate.created_at = model.created_at;
            bookUpdate.updated_at = model.updated_at;

            // Đánh dấu bản ghi là đã thay đổi
            _context.book.Update(bookUpdate);

            return saveChange();
        }
    }
}
