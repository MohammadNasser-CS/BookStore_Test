using ApiExample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Data
{
    public class BookStoreApiContext : DbContext
    {
        public BookStoreApiContext(DbContextOptions<BookStoreApiContext> options)
            : base(options)
        { }
        public DbSet<Book> Books { get; set; }
    }
}
