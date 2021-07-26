using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Identity;
using TweetBook.Infrastructure.DTO;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Identity
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<AuthenticationResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            return authResponse;
        }
    }
}
