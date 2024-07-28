using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Dtos.Book
{
    public class CreateBookRequestDto
    {
        public string Title { get; set; } = string.Empty;
    }
}