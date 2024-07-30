using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Category;
using ApiExample.Interfaces;
using ApiExample.Mapper;
using ApiExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryRepository.GetAllAsync();
            var categoryDtos = categories.Select(C => C.ToCategoryDto());
            return Ok(new { Categories = categoryDtos });
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(new { Category = category });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto createCategory)
        {
            var categoryModel = createCategory.CreateCategoryDto();
            await categoryRepository.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequest)
        {
            var categoryModel = await categoryRepository.UpdateAsync(id, updateCategoryRequest);
            if (categoryModel == null) return NotFound();

            return Ok(categoryModel);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryModel = await categoryRepository.DeleteAsyny(id);
            if (categoryModel == null) return NotFound();
            return NoContent();
        }
    }
}