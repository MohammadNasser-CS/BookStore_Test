using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Interfaces;
using ApiExample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreApiContext context;

        public BookRepository(BookStoreApiContext context)
        {
            this.context = context;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await context.Books.ToListAsync();
        }
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await context.Books.FindAsync(id);
        }
        public async Task<Book> CreateAsync(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return book;
        }
        public async Task<Book?> UpdateAsync(int id, UpdateBookRequestDto updateBook)
        {
            var existingBook = await context.Books.FirstOrDefaultAsync(B => B.BookId == id);
            if (existingBook == null) return null;
            existingBook.Title = updateBook.Title;
            existingBook.Description = updateBook.Description;
            existingBook.Price = updateBook.Price;
            await context.SaveChangesAsync();
            return existingBook;
        }
        public async Task<Book?> DeleteAsyny(int id)
        {
            var deletedBook = await context.Books.FirstOrDefaultAsync(B => B.BookId == id);
            if (deletedBook == null) return null;
            context.Books.Remove(deletedBook);
            await context.SaveChangesAsync();
            return deletedBook;
        }

    }
}