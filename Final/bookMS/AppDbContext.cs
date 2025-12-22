using bookMS.Models;
using Microsoft.EntityFrameworkCore;
namespace bookMS
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<Genre>()
            .HasIndex(g => g.Name)
            .IsUnique();

            modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User);

            modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books);

            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order);

            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Book)
            .WithMany(b => b.OrderItems);
        }
    }
}