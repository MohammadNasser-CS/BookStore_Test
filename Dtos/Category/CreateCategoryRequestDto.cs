using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Dtos.Category
{
    public class CreateCategoryRequestDto
    {
        [Required(ErrorMessage = "Name field is requierd")]
        [MinLength(5, ErrorMessage = "Name length must be at least 5 Characters")]
        [MaxLength(15, ErrorMessage = "Name length must be maximum 15 Characters")]
        [DataType(DataType.Text, ErrorMessage = "Name must be a string")]
        public string Name { get; set; } = String.Empty;
    }
}