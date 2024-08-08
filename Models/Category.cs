using System.ComponentModel.DataAnnotations.Schema;

namespace ApiExample.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        [InverseProperty("Category")]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>(); // Navegational Property: M.
    }
}
