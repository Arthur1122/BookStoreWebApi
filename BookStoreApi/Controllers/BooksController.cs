using BookStoreApi.DTOs;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly AuthorsService _authorsService;

        public BooksController(BooksService booksService, AuthorsService authorsService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<List<Book>> Get() =>
           await _booksService.GetAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookDto newBook)
        {
            // Check Author Id
            var author = await _authorsService.GetAsync(newBook.AuthorId);

            if (author is null)
            {
                return NotFound($"Author with '{newBook.AuthorId}' not found");
            }


            var book = new Book
            {
                Id = $"{Guid.NewGuid()}",
                BookName = newBook.Name,
                Price = newBook.Price,
                Category = newBook.Category,
                Author = author,
            };

            await _booksService.CreateAsync(book);

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, BookDto updatedBook)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            var author = await _authorsService.GetAsync(updatedBook.AuthorId);

            if (author is null)
            {
                return NotFound($"Author with '{updatedBook.AuthorId}' not found");
            }

            book.BookName = updatedBook.Name;
            book.Price = updatedBook.Price;
            book.Category = updatedBook.Category;
            book.Author = author;

            await _booksService.UpdateAsync(id, book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
