using BookStoreApi.DTOs;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<List<Author>> Get() =>
           await _authorsService.GetAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(string id)
        {
            var Author = await _authorsService.GetAsync(id);

            if (Author is null)
            {
                return NotFound();
            }

            return Author;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorDto newAuthor)
        {
            if (string.IsNullOrWhiteSpace(newAuthor.FirstName))
            {
                return BadRequest("FirstName is required.");
            }

            if (string.IsNullOrWhiteSpace(newAuthor.LastName))
            {
                return BadRequest("LastName is required.");
            }

            var author = new Author
            {
                Id = $"{Guid.NewGuid()}",
                FirstName = newAuthor.FirstName,
                LastName = newAuthor.LastName
            };

            await _authorsService.CreateAsync(author);

            return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AuthorDto updatedAuthor)
        {
            var author = await _authorsService.GetAsync(id);

            if (author is null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedAuthor.FirstName))
            {
                return BadRequest("FirstName is required.");
            }

            if (string.IsNullOrWhiteSpace(updatedAuthor.LastName))
            {
                return BadRequest("LastName is required.");
            }

            author.FirstName = updatedAuthor.FirstName;
            author.LastName = updatedAuthor.LastName;

            await _authorsService.UpdateAsync(id, author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Author = await _authorsService.GetAsync(id);

            if (Author is null)
            {
                return NotFound();
            }

            await _authorsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
