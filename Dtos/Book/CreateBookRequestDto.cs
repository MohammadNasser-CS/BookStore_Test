using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Dtos.Book
{
    public class CreateBookRequestDto
    {
        [Required(ErrorMessage = "Title field is requierd")]
        [MinLength(5, ErrorMessage = "Title length must be at least 5 Characters")]
        [MaxLength(15, ErrorMessage = "Title length must be maximum 15 Characters")]
        [DataType(DataType.Text, ErrorMessage = "Title must be a string")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description field is requierd")]
        [MinLength(15, ErrorMessage = "Description length must be at least 15 Characters")]
        [MaxLength(100, ErrorMessage = "Description length must be maximum 100 Characters")]
        [DataType(DataType.Text, ErrorMessage = "Description must be a string")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price field is requierd")]
        [Range(1, 10000)]
        [DataType(DataType.Currency, ErrorMessage = "Price must be a currence")]
        public double Price { get; set; }
    }
}