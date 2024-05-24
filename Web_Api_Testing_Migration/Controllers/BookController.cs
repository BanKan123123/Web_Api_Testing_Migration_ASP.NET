using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<book>))]
        public IActionResult GetBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.findAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(books);
        }

        [HttpGet("{bookSlug}")]
        [ProducesResponseType(200, Type = typeof(book))]
        [ProducesResponseType(400)]
        public IActionResult GetBookSlug(string slug)
        {
            if (!_bookRepository.isExistBySlug(slug))
            {
                return NotFound();
            }
            var book = _mapper.Map<BookDto>(_bookRepository.findOneBySlug(slug));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(book);
        }

        [HttpGet("{bookId}/chapter")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<chapter>))]
        [ProducesResponseType(400)]
        public IActionResult GetChaptersById(int id)
        {
            if (!_bookRepository.isExistById(id))
            {
                return NotFound();
            }
            var chapters = _mapper.Map<List<ChapterDto>>(_bookRepository.getChaptersById(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(chapters);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateBook([FromQuery] int authorId, [FromBody] BookDto bookDto)
        {
            if (bookDto == null) return BadRequest(ModelState);

            var bookCheck = _bookRepository.findAll().FirstOrDefault(b => b.slug == bookDto.slug);

            if (bookCheck != null)
            {
                ModelState.AddModelError("Message", "Book already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<book>(bookDto);
            var authorEntity = _authorRepository.findOneById(authorId);
            if (authorEntity == null)
            {
                ModelState.AddModelError("", "Author not found");
                return NotFound(ModelState);
            }
            book.author = authorEntity;

            // Create book and get its Id
            var createdBookId = _bookRepository.createBook(book);

            // Check if book creation failed
            if (createdBookId == 0)
            {
                ModelState.AddModelError("", "Something went wrong while creating book");
                return StatusCode(500, ModelState);
            }

            // Handle the list of categoriesonbook and save changes
            var categoriesOnBooks = new List<categoriesonbook>();
            foreach (var categoryDto in bookDto.categories)
            {
                var categoryEntity = _categoryRepository.findOneById(categoryDto.id);
                if (categoryEntity == null)
                {
                    ModelState.AddModelError("", $"Category with id {categoryDto.id} not found");
                    return NotFound(ModelState);
                }

                categoriesOnBooks.Add(new categoriesonbook
                {
                    bookId = createdBookId,
                    categoryId = categoryDto.id,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                });
            }

            // Check ModelState before adding categories
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_categoryRepository.CreateCategoriesOnBook(categoriesOnBooks))
            {
                ModelState.AddModelError("", "Something went wrong while adding categories to book");
                return StatusCode(500, ModelState);
            }

            return Ok("Creating book successfully");
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateBook(int id, [FromQuery] int authorId, [FromBody] BookDto bookDto)
        {
            if (bookDto == null) return BadRequest(ModelState);

            if (id != bookDto.id) return BadRequest("Slug in the URL does not match slug in the body.");

            var bookOld = _bookRepository.findOneById(id);
            if (bookOld == null) return NotFound();

            var authorEntity = _authorRepository.findOneById(authorId);
            if (authorEntity == null)
            {
                ModelState.AddModelError("", "Author not found");
                return NotFound(ModelState);
            }

            // Map dữ liệu từ bookDto sang bookMap
            var bookMap = _mapper.Map<book>(bookDto);
            bookMap.id = bookOld.id; // Đảm bảo rằng Id được gán đúng
            bookMap.author = authorEntity;

            if (bookOld.categories != null && bookOld.categories.Count > 0)
            {
                if (!_categoryRepository.DeleteCategoriesOnBook(bookOld.categories))
                {
                    ModelState.AddModelError("", $"Something went wrong while deleting categories on book {id}");
                    return StatusCode(500, ModelState);
                }
            }

            // Cập nhật thông tin book
            if (!_bookRepository.updateBySlug(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating book");
                return StatusCode(500, ModelState);
            }

            //Xóa các categoriesonbook cũ


            // Tạo danh sách mới categoriesonbook
            var categoriesOnBooks = new List<categoriesonbook>();
            foreach (var categoryDto in bookDto.categories)
            {
                var categoryEntity = _categoryRepository.findOneById(categoryDto.id);
                if (categoryEntity == null)
                {
                    ModelState.AddModelError("", $"Category with id {categoryDto.id} not found");
                    return NotFound(ModelState);
                }

                categoriesOnBooks.Add(new categoriesonbook
                {
                    bookId = id,
                    categoryId = categoryDto.id,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                });
            }

            // Thêm danh sách mới categoriesonbook
            if (!_categoryRepository.CreateCategoriesOnBook(categoriesOnBooks))
            {
                ModelState.AddModelError("", "Something went wrong while adding categories to book");
                return StatusCode(500, ModelState);
            }
            return Ok("Updating book successfully");
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult DeleteBook(int bookId)
        {
            if (!_bookRepository.isExistById(bookId))
            {
                ModelState.AddModelError("", "Book not found");
                return BadRequest(ModelState);
            }

            var bookDelete = _bookRepository.findOneById(bookId);

            if (bookDelete == null)
            {
                ModelState.AddModelError("", "Book not found");
                return NotFound(ModelState);
            }

            // Check if categories are not empty before attempting to delete them
            if (bookDelete.categories != null && bookDelete.categories.Count > 0)
            {
                if (!_categoryRepository.DeleteCategoriesOnBook(bookDelete.categories))
                {
                    ModelState.AddModelError("", $"Something went wrong while deleting categories on book {bookDelete.categories}");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_bookRepository.deleteOneBySlug(bookDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the book");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleting book successfully");
        }
    }
}
