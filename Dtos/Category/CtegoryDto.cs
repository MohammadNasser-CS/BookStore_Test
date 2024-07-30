using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiExample.Dtos.Book;

namespace ApiExample.Dtos.Category
{
    public class CategoryDto
    {
        public string Name { get; set; } = String.Empty;
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}