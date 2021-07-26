using MediatR;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Contracts.V1.Commands.Identity
{
    public class UserLoginCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
