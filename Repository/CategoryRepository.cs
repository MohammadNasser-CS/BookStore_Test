using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Data;
using ApiExample.Dtos.Book;
using ApiExample.Dtos.Category;
using ApiExample.Interfaces;
using ApiExample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreApiContext context;

        public CategoryRepository(BookStoreApiContext context)
        {
            this.context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.Include(C => C.Books).ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await context.Categories.Include(C => C.Books).FirstOrDefaultAsync(C => C.Id == id);
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }
        public async Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryRequestDto)
        {
            var existingCategory = await context.Categories.FirstOrDefaultAsync(C => C.Id == id);
            if (existingCategory == null) return null;
            existingCategory.Name = categoryRequestDto.Name;
            await context.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<Category?> DeleteAsyny(int id)
        {
            var deletedCategory = await context.Categories.FirstOrDefaultAsync(C => C.Id == id);
            if (deletedCategory == null) return null;
            context.Categories.Remove(deletedCategory);
            await context.SaveChangesAsync();
            return deletedCategory;
        }
        public Task<bool> CategoryExists(int id)
        {
            return context.Categories.AnyAsync(C => C.Id == id);
        }
    }
}