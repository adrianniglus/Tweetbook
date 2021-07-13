using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        

        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpPost(Contracts.V1.ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            

            if (postRequest.Id == Guid.Empty)
                postRequest.Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(postRequest.Name))
                postRequest.Name = "Example name";

            await _postService.CreatePostAsync(postRequest.Id, postRequest.Name);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + Contracts.V1.ApiRoutes.Posts.Get.Replace("{postId}", postRequest.Id.ToString());

            var response = new PostResponse
            {
                Id = postRequest.Id,
                Name = postRequest.Name

            };
            
            return Created(locationUri,response);
        }

        [HttpPut(TweetBook.Contracts.V1.ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest postRequest)
        {
            var updated = await _postService.UpdatePostAsync(postId, postRequest);

            var response = new PostResponse
            {
                Id = postId,
                Name = postRequest.Name
            };
            if (updated)
                return Ok(response);
            return NotFound();
        }

        [HttpDelete(Contracts.V1.ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
