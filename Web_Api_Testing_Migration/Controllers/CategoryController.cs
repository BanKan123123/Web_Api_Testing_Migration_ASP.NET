using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.findAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{categorySlug}")]
        [ProducesResponseType(200, Type = typeof(category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryBySlug(string slug)
        {
            if (!_categoryRepository.isExistBySlug(slug))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_categoryRepository.findOneBySlug(slug));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(category);
        }
        [HttpGet("book/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookByCategory(int id)
        {
            var books = _categoryRepository.getBookByCategory(id);

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookDtos);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateCatgory([FromBody] CategoryDto category)
        {
            if (category == null) return BadRequest(ModelState); ;

            var checkCategory = _categoryRepository.findAll().Where(c => c.slug.Trim() == category.slug.Trim()).FirstOrDefault();

            if (checkCategory != null)
            {
                ModelState.AddModelError("Message", "Category already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryCreate = _mapper.Map<category>(category);

            if (!_categoryRepository.create(categoryCreate))
            {
                ModelState.AddModelError("Message", "Something went wrong while creating category process");
                return StatusCode(500, ModelState);
            }
            return Ok("Message: Creating category successfully");
        }


        [HttpDelete("${categorySlug}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCategory(string slug)
        {
            if (!_categoryRepository.isExistBySlug(slug)) return NotFound();

            var categoryToDelete = _categoryRepository.findOneBySlug(slug);

            if (!ModelState.IsValid) return BadRequest();

            if (!_categoryRepository.deleteOneBySlug(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting category");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleting category successfully");
        }

        [HttpPut("{categorySlug}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateCategory(string slug, [FromBody] CategoryDto category)
        {
            if (category == null) return BadRequest(ModelState);

            if (slug != category.slug) return BadRequest(ModelState);

            if (!_categoryRepository.isExistBySlug(slug)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var categoryMap = _mapper.Map<category>(category);

            if (!_categoryRepository.updateBySlug(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Message: Updating category successfully");
        }

    }
}
