using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Interfaces;
using ApiExample.Mapper;
using ApiExample.Models;
using ApiExample.Repository;
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
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookRepository.GetAllAsync();
            var booksDto = books.Select(S => S.ToBookDto());
            return Ok(new { books = booksDto });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new { book = book.ToBookDto() });
        }
        [HttpPost("{categoryId}")]
        public async Task<IActionResult> Create([FromRoute] int categoryId, [FromBody] CreateBookRequestDto createBook)
        {
            if (!await categoryRepository.CategoryExists(categoryId))
            {
                return BadRequest("Category Does Not Exist");
            }
            var bookModel = createBook.CreateBookDto(categoryId);
            await bookRepository.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookRequest)
        {
            var bookModel = await bookRepository.UpdateAsync(id, updateBookRequest);
            if (bookModel == null) return NotFound();

            return Ok(bookModel);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookModel = await bookRepository.DeleteAsyny(id);
            if (bookModel == null) return NotFound();
            return NoContent();
        }
    }
}
