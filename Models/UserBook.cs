using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Models
{
    public class UserBook
    {
        public string UserId { get; set; }
        public virtual User User { get; set; } 
        public int BookId { get; set; }
        public virtual Book Book { get; set; } 
    }
}