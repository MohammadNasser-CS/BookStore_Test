using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Mapper;
using ApiExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreApiContext context;
        public BookController(BookStoreApiContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var books = await context.Books.ToListAsync();
            var books = await context.Books.ToListAsync();
            var booksDto = books.Select(S => S.ToBookDto());
            return Ok(new { books = books });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new { book = book.ToBookDto() });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto createBook)
        {
            var bookModel = createBook.CreateBookDto();
            await context.Books.AddAsync(bookModel);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookRequest)
        {
            var bookModel = await context.Books.FirstOrDefaultAsync(B => B.BookId == id);
            if (bookModel == null) return NotFound();
            bookModel.Title = updateBookRequest.Title;
            await context.SaveChangesAsync();
            return Ok(bookModel);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookModel = await context.Books.FirstOrDefaultAsync(B => B.BookId == id);
            if (bookModel == null) return NotFound();
            context.Books.Remove(bookModel);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
