using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.DTO;
using TweetBook.Infrastructure.Extensions;
using TweetBook.Infrastructure.Services;
using Mapster;
using MediatR;
using TweetBook.Contracts.V1.Queries;
using TweetBook.Contracts.V1.Commands;

namespace TweetBook.Controllers.V1
{
    
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMediator _mediator;
        public PostsController(IPostService postService, IMediator mediator)
        {
            _postService = postService;
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
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest postRequest)
        {

            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if(!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post"});
            }
            var post = await _postService.GetPostByIdAsync(postId);

            post.Name = postRequest.Name;

            var updated = await _postService.UpdatePostAsync(post);

            

            if (updated)
                return Ok(post);
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }


            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }


    }
}
