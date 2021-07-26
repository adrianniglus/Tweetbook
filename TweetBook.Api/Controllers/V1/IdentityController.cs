using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Identity;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Api.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var result = await _mediator.Send(command);

            return ValidateAuthResponse(result);
        }

        

        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            var result = await _mediator.Send(command);

            return ValidateAuthResponse(result);
        }

        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);

            return ValidateAuthResponse(result);

        }

        private IActionResult ValidateAuthResponse(AuthenticationResult authResponse)
        {
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
