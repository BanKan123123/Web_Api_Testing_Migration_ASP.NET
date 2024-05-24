using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<author>))]
        public IActionResult GetAuthors()
        {

            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.findAll());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(authors);
        }

        [HttpGet("{authorId}/author")]
        [ProducesResponseType(200, Type = typeof(author))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorById(int id)
        {
            if (!_authorRepository.isExistById(id))
                return NotFound();

            var author = _authorRepository.findOneById(id);

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(author);
        }

        [HttpGet("{authorSlug}")]
        [ProducesResponseType(200, Type = typeof(author))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorBySlug(string slug)
        {
            if (!_authorRepository.isExistBySlug(slug))
                return NotFound();

            var author = _mapper.Map<AuthorDto>(_authorRepository.findOneBySlug(slug));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateAuthor([FromBody] AuthorDto author)
        {
            if (author == null)
                return BadRequest(ModelState);

            var checkAuthor = _authorRepository.findAll().Where(ar => ar.slug.Trim() == author.slug.Trim()).FirstOrDefault();

            if (checkAuthor != null)
            {
                ModelState.AddModelError("Message", "Author already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorCreated = _mapper.Map<author>(author);
            if (!_authorRepository.create(authorCreated))
            {
                ModelState.AddModelError("Message:", "Something went wrong while saved process");
                return StatusCode(500, ModelState);
            }
            return Ok("Message: Creating author succesfully");
        }

        [HttpPut("{authorSlug}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateAuthor(string slug, [FromBody] AuthorDto author)
        {
            if (author == null) return BadRequest(ModelState);

            if (slug != author.slug) return BadRequest(ModelState);

            if (!_authorRepository.isExistBySlug(slug)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var authorMap = _mapper.Map<author>(author);

            if (!_authorRepository.updateBySlug(authorMap))
            {
                ModelState.AddModelError("Message", "Something went wrong while updating author process");
                return StatusCode(500, ModelState);
            }
            return Ok("Message: Updating author successfully");
        }

        [HttpDelete("{authorSlug}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult DeleteAuthor(string slug)
        {
            if (!_authorRepository.isExistBySlug(slug)) return NotFound();

            var authorToDelete = _authorRepository.findOneBySlug(slug);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_authorRepository.deleteOneBySlug(authorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting author");
                return StatusCode(500, ModelState);
            }
            return Ok("Deleting author succesfully");
        }
    }
}
