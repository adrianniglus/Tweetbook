using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Identity;
using TweetBook.Infrastructure.DTO;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Identity
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;

        public UserLoginHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthenticationResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            return authResponse;
        }
    }
}
