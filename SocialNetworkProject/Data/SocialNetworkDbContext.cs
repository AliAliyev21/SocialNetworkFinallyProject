using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Entities;
using System;
using System.Linq;

namespace SocialNetworkProject.Data
{
    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<LiveChat> LiveChats { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Post> Posts { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Friend>().HasKey(f => f.Id);

            modelBuilder.Entity<Message>()
               .HasOne(m => m.Sender)
               .WithMany(u => u.SentMessages)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
