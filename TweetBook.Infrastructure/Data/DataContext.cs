using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetBook.Domain.Entities;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<RefreshToken> RefreshTokens {get; set;}
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasOne(x => x.Post)
                      .WithMany(x => x.Tags)
                      .HasForeignKey(x => x.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
