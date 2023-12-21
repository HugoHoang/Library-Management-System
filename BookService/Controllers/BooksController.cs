using AutoMapper;
using BookService.Data;
using BookService.Dtos;
using BookService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _repo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetBooks()
        {
            Console.WriteLine("--> Getting all books ");
            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(_repo.GetAllBooks()));
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public ActionResult<BookReadDto> GetBookById(int id)
        {
            Console.WriteLine("--> Getting one book");
            var bookItem = _repo.GetBookById(id);

            return bookItem == null ? NotFound() : Ok(_mapper.Map<BookReadDto>(bookItem));
        }

        [HttpGet("{bookId}/available-count")]
        public IActionResult GetAvailableCount(int bookId)
        {
            try
            {
                int availableCount = _repo.GetAvailableCount(bookId);
                return Ok(new { AvailableCount = availableCount });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> AddBook(BookCreateDto bookDto)
        {
            Console.WriteLine("--> Adding book");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var bookModel = _mapper.Map<Book>(bookDto);
                await _repo.AddBook(bookModel);
                _repo.SaveChanges();

                var bookReadDto = _mapper.Map<BookReadDto>(bookModel);
                return CreatedAtRoute(nameof(GetBookById), new { id = bookReadDto.bookId }, bookReadDto);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{bookId}")]
        public ActionResult<BookUpdateDto> UpdateBookCount(int bookId, BookUpdateDto bookDto)
        {
            if (bookId != bookDto.bookId)
            {
                return BadRequest("Mismatched book ID.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repo.UpdateBook(bookDto.bookId, bookDto.AvailableCount);
                _repo.SaveChanges();
                var bookAddCountDto = _mapper.Map<BookUpdateDto>(_repo.GetBookById(bookDto.bookId));
                return CreatedAtRoute(nameof(GetBookById), new { id = bookAddCountDto.bookId }, bookAddCountDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}