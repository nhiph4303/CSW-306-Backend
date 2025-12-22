using BookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Name of Genre is unique
            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            //**Relationship**

            // 1 User - Many Orders
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);

            // 1 Genre - Many Books
            modelBuilder.Entity<Book>()
               .HasOne(b => b.Genre)
               .WithMany(g => g.Books);

            // 1 Order - Many OrderItems
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order);

            // 1 Book - Many OrderItems
            modelBuilder.Entity<OrderItem>()
              .HasOne(oi => oi.Book)
              .WithMany(b => b.OrderItems);

            // ========== SEED DATA ==========
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   FullName = "Admin User",
                   Email = "admin@eduvision.com",
                   Password = "admin123",
                   Role = "Admin",
                   CreatedAt = DateTime.Now
               },
               new User
               {
                   Id = 2,
                   FullName = "Test User",
                   Email = "user@eduvision.com",
                   Password = "user123",
                   Role = "User",
                   CreatedAt = DateTime.Now
               }
           );
        }
    }
}
