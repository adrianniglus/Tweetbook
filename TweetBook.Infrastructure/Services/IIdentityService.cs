using System.Threading.Tasks;
using TweetBook.Domain.Entities;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public interface IIdentityService
    {
        Task<DTO.AuthenticationResult> RegisterAsync(string email, string password);
        Task<DTO.AuthenticationResult> LoginAsync(string email, string password);
        Task<DTO.AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
