using Microsoft.EntityFrameworkCore;
using Greenhouse.Models;
namespace Greenhouse.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
// User -> SensorReadings relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.SensorReadings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Messages relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Message properties configuration
            modelBuilder.Entity<Message>()
                .Property(m => m.UserMessage)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            modelBuilder.Entity<Message>()
                .Property(m => m.BotResponse)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            // User properties configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            // Indexes for performance
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Message>()
                .HasIndex(m => m.Timestamp);
        }
            


        // Constructor with DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
