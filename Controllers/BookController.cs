using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Mapper;
using ApiExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var books = context.Books.ToList();
            // var books = context.Books.ToList().Select(S => S.ToBookDto());
            return Ok(new { books = books });
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new { book = book.ToBookDto() });
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateBookRequestDto createBook)
        {
            var bookModel = createBook.CreateBookDto();
            context.Books.Add(bookModel);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookRequest)
        {
            var bookModel = context.Books.FirstOrDefault(B => B.BookId == id);
            if (bookModel == null) return NotFound();
            bookModel.Title = updateBookRequest.Title;
            context.SaveChanges();
            return Ok(bookModel);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var bookModel = context.Books.FirstOrDefault(B => B.BookId == id);
            if (bookModel == null) return NotFound();
            context.Books.Remove(bookModel);
            context.SaveChanges();
            return NoContent();
        }
    }
}
