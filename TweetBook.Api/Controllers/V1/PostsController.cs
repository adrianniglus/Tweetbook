using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediatR;
using TweetBook.Contracts.V1.Commands.Posts;
using TweetBook.Contracts.V1.Queries.Posts;

namespace TweetBook.Controllers.V1
{
    
    public class PostsController : Controller
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var query = new GetPostByIdQuery(postId);

            var result =  await _mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var querry = new GetAllPostsQuery();
            var result = await _mediator.Send(querry);

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(Contracts.V1.ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        {

            var result = await _mediator.Send(command);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + Contracts.V1.ApiRoutes.Posts.Get.Replace("{postId}", result.Id.ToString());

            

            return Created(locationUri, result);


        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(TweetBook.Contracts.V1.ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostCommand command)
        {

            var mergedCommand = new UpdatePostMergedCommand
            {
                PostId = postId,
                Name = command.Name
            };

            var result = await _mediator.Send(mergedCommand);


            if (result == null)
            {
                return BadRequest(new { error = "You do not own this post or it doesn't exist!" });
            }



            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] DeletePostCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
                return NoContent();

            return BadRequest(new { error = "You do not own this post or it doesn't exist!" });

            //return NotFound();
        }


    }
}
