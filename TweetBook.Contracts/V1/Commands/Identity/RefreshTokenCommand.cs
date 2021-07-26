using MediatR;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Contracts.V1.Commands.Identity
{
    public class RefreshTokenCommand : IRequest<AuthenticationResult>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
