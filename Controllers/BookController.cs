using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Helpers;
using ApiExample.Interfaces;
using ApiExample.Mapper;
using ApiExample.Models;
using ApiExample.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAll([FromQuery] BookQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var books = await bookRepository.GetAllAsync(query);
            var booksDto = books.Select(S => S.ToBookDto());
            return Ok(new { books = booksDto });
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(new { book = book.ToBookDto() });
        }
        [HttpPost("{categoryId:int}")]
        public async Task<IActionResult> Create([FromRoute] int categoryId, [FromBody] CreateBookRequestDto createBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await categoryRepository.CategoryExists(categoryId))
            {
                return BadRequest("Category Does Not Exist");
            }
            var bookModel = createBook.CreateBookDto(categoryId);
            await bookRepository.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookModel = await bookRepository.UpdateAsync(id, updateBookRequest);
            if (bookModel == null) return NotFound();

            return Ok(bookModel);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookModel = await bookRepository.DeleteAsyny(id);
            if (bookModel == null) return NotFound();
            return NoContent();
        }
    }
}
