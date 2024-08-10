using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Data;
using ApiExample.Interfaces;
using ApiExample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Repository
{
    public class UserBookRepository : IUserBookRepository
    {
        private readonly BookStoreApiContext _dbContext;
        public UserBookRepository(BookStoreApiContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Book>> GetUserBooks(User user)
        {
            return await _dbContext.UserBooks.Where(u => u.UserId == user.Id)
            .Select(book => new Book
            {
                BookId = book.BookId,
                Title = book.Book.Title,
                Category = book.Book.Category,
                Description = book.Book.Description,
                Price = book.Book.Price,
            }).ToListAsync();
        }
    }
}