using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<PostDTO> Posts { get; set; }
        public DbSet<RefreshTokenDTO> RefreshTokens {get; set;}
    }
}
