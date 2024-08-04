using ApiExample.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.Data
{
    public class BookStoreApiContext : IdentityDbContext<User>
    {
        public BookStoreApiContext(DbContextOptions<BookStoreApiContext> options)
            : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
