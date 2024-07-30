using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Book;
using ApiExample.Dtos.Category;
using ApiExample.Models;

namespace ApiExample.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task<Category> CreateAsync(Category category);
        public Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto updateBook);
        public Task<Category?> DeleteAsyny(int id);
         Task<bool> CategoryExists(int id);
    }
}