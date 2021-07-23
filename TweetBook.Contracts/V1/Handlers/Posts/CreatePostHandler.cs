using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Posts;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Models;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Posts
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
            var id = Guid.NewGuid();
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var created = await _postService.CreatePostAsync(id, request.Name,userId , request.Tags.Adapt<List<TagModel>>());

            if(created == false)
            {
                return null;
            }


            var post = await _postService.GetPostByIdAsync(id);

            return post.Adapt<PostResponse>();
        }
    }
}
