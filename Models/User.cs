using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiExample.Models
{
    public class User : IdentityUser
    {
        [InverseProperty("User")]
        public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    }
}