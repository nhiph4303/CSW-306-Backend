using LeThiMinhTuoi_Lab7_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LeThiMinhTuoi_Lab7_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Book> Books { get; set; }
    }
}
