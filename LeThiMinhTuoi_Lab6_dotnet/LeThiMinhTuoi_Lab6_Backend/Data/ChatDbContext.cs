using LeThiMinhTuoi_Lab6_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeThiMinhTuoi_Lab6_Backend.Data
{
        public class ChatDbContext : IdentityDbContext<AppUser>
        {
            public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
            {

            }

            public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
