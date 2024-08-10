using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Models
{
    public class UserBook
    {
        [AllowNull]
        public string UserId { get; set; }
        [AllowNull]
        public virtual User User { get; set; }
        public int BookId { get; set; }
        [AllowNull]
        public virtual Book Book { get; set; }
    }
}