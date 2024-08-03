using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Helpers;
using ApiExample.Models;

namespace ApiExample.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllAsync(BookQueryObject query);
        public Task<Book?> GetByIdAsync(int id);
        public Task<Book> CreateAsync(Book book);
        public Task<Book?> UpdateAsync(int id, UpdateBookRequestDto updateBook);
        public Task<Book?> DeleteAsyny(int id);
    }
}