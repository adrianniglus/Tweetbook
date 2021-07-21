using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Models;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostResponse>
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePostHandler(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            //
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var id = Guid.NewGuid();
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var created = await _postService.CreatePostAsync(id, request.Name,userId , request.Tags.Adapt<List<TagModel>>());

            if(created == false)
            {
                return null;
            }

            var post = new PostResponse
            {
                Id = id,
                Name = request.Name,
                UserId = userId,
                Tags = request.Tags.Adapt<List<TagResponse>>()
            };

            return post;
        }
    }
}
