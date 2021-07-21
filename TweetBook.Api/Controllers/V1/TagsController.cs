using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.DTO;
using TweetBook.Infrastructure.Extensions;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Api.Controllers.V1
{
    
    public class TagsController : Controller
    {
        private readonly IPostService _postService;

        public TagsController(IPostService postService)
        {
            _postService = postService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "MustWorkForGoogle")]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _postService.GetAllTagsAsync();
            return Ok(tags);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateAndAddTagToPostRequest tagRequest)
        {
            var tag = new TagDTO
            {
                TagName = tagRequest.TagName,
                PostId = tagRequest.PostId
            };



            await _postService.CreateTagAsync(tag);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + Contracts.V1.ApiRoutes.Tags.Get.Replace("{tagId}", tag.Id.ToString());

            var response = new TagResponse
            {
                Id = tag.Id,
                TagName =  tag.TagName,
                PostId = tag.PostId
            };

            return Created(locationUri, response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid tagId)
        {
            var tag = await _postService.GetTagByIdAsync(tagId);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(Contracts.V1.ApiRoutes.Tags.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid tagId)
        {
            var tag = await _postService.GetTagByIdAsync(tagId);

            var userOwnsPost = await _postService.UserOwnsPostAsync(tag.PostId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post/tag" });
            }


            var deleted = await _postService.DeleteTagAsync(tagId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

    }
}
