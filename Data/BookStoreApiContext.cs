using ApiExample.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<UserBook> UserBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserBook>(UB => UB.HasKey(key => new { key.BookId, key.UserId }));
            // from the perspective of the User and Book entities.
            builder.Entity<Book>()
                .HasMany(UB => UB.UserBooks)
                .WithOne(B => B.Book)
                .HasForeignKey(F => F.BookId);
            builder.Entity<User>()
                .HasMany(UB => UB.UserBooks)
                .WithOne(U => U.User)
                .HasForeignKey(F => F.UserId);

            #region From the perspective of the UserBook entity:
            // builder.Entity<UserBook>()
            //     .HasOne(u => u.User)
            //     .WithMany(u => u.UserBooks)
            //     .HasForeignKey(p => p.UserId);
            // builder.Entity<UserBook>()
            //     .HasOne(u => u.Book)
            //     .WithMany(u => u.UserBooks)
            //     .HasForeignKey(p => p.BookId);
            #endregion

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Author",
                    NormalizedName = "AUTHOR"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
