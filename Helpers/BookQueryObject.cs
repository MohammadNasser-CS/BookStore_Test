using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Helpers
{
    public class BookQueryObject
    {
        public String? Title { get; set; } = null;
        public double? Price { get; set; } = null;
        public String? Category { get; set; } = null;
        public String? SortBy { get; set; } = null;
        public bool IsDesc { get; set; } = false;
        public int PageNumber { get; set; }=1;
        public int PageSize { get; set; }=2;
    }
}