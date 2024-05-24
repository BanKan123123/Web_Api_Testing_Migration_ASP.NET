using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : Controller
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ChapterController(IChapterRepository chapterRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _chapterRepository = chapterRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<chapter>))]
        public IActionResult GetChapters()
        {
            var chapters = _mapper.Map<List<ChapterDto>>(_chapterRepository.findAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chapters);
        }

        [HttpGet("chapterSlug")]
        [ProducesResponseType(200, Type = typeof(chapter))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetChapterBySlug(string slug)
        {
            if (!_chapterRepository.isExistBySlug(slug)) return NotFound();

            var chapter = _mapper.Map<ChapterDto>(_chapterRepository.findOneBySlug(slug));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(chapter);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult CreateChapter([FromQuery] int bookId, [FromBody] ChapterDto chapter)
        {
            if (chapter == null) return BadRequest(ModelState);

            var chapterCheck = _chapterRepository.findAll()
                .Where(c => c.slug.Trim() == chapter.slug.Trim())
                .FirstOrDefault();

            if (chapterCheck != null)
            {
                ModelState.AddModelError("", "Chapter Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var chapterMap = _mapper.Map<chapter>(chapter);
            var bookEntity = _bookRepository.findOneById(bookId);
            if (bookEntity  == null)
            {
                ModelState.AddModelError("", "Book not found");
                return NotFound(ModelState);
            }

            chapterMap.book = _mapper.Map<book>(bookEntity);

            if (!_chapterRepository.create(chapterMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating chapter");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{chapterSlug}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateChapterBySlug(string slug,[FromQuery] int bookId, [FromBody] ChapterDto chapter)
        {
            if (chapter == null) return BadRequest(ModelState);

            if (slug != chapter.slug) return BadRequest(ModelState);

            if (!_chapterRepository.isExistBySlug(slug)) return NotFound();

            var chapterMap = _mapper.Map<chapter>(chapter);
            chapterMap.book = _bookRepository.findOneById(bookId);

            if (!_chapterRepository.updateBySlug(chapterMap))
            {
                ModelState.AddModelError("Message", "Something went wrong while updating chapter process");
                return StatusCode(500, ModelState);
            }

            return Ok("Message : Updating chapter successfully");
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult DeleteChapter(string slug)
        {
            if (!_chapterRepository.isExistBySlug(slug)) return NotFound();

            var chapterDelete = _chapterRepository.findOneBySlug(slug);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_chapterRepository.deleteOneBySlug(chapterDelete))
            {
                ModelState.AddModelError("", "Something went wrong while delete chapter ");
                return StatusCode(500, ModelState);
            }
            return Ok("Message: Deleting chapter Successfully");
        }
    }
}
