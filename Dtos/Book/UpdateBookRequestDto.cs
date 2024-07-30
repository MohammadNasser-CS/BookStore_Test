using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Dtos.Book
{
    public class UpdateBookRequestDto
    {
        [Required(ErrorMessage = "Title field is requierd")]
        [MinLength(5, ErrorMessage = "Title length must be at least 5 Characters")]
        [MaxLength(15, ErrorMessage = "Title length must be maximum 15 Characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description field is requierd")]
        [MinLength(15, ErrorMessage = "Description length must be at least 15 Characters")]
        [MaxLength(100, ErrorMessage = "Description length must be maximum 100 Characters")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price field is requierd")]
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}