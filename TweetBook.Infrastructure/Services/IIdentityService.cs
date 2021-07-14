using System.Threading.Tasks;
using TweetBook.Domain.Entities;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResultDTO> RegisterAsync(string email, string password);
        Task<AuthenticationResultDTO> LoginAsync(string email, string password);
        Task<AuthenticationResultDTO> RefreshTokenAsync(string token, string refreshToken);
    }
}
