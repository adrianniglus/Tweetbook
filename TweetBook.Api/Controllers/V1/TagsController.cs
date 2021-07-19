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
            return Ok(await _postService.GetAllTagsAsync());
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            var tag = new TagDTO
            {
                TagName = tagRequest.TagName                
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
    }
}
