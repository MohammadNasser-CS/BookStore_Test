using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Category;
using ApiExample.Models;

namespace ApiExample.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Name = category.Name,
                Books = category.Books.Select(C => C.ToBookDto()).ToList(),
            };
        }

        public static Category CreateCategoryDto(this CreateCategoryRequestDto createCategoryDto)
        {
            return new Category
            {
                Name = createCategoryDto.Name,
            };
        }

        public static Category UpdateCategoryDto(this UpdateCategoryRequestDto updateCategoryDto)
        {
            return new Category
            {
                Name = updateCategoryDto.Name,
            };
        }
    }
}