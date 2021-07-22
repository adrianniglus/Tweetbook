using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostMergedCommand, PostResponse>
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdatePostHandler(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PostResponse> Handle(UpdatePostMergedCommand request, CancellationToken cancellationToken)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var userOwnsPost = await _postService.UserOwnsPostAsync(request.PostId, userId);

            if (!userOwnsPost)
            {
                return null;
            }

            var updated = await _postService.UpdatePostAsync(request.PostId, request.Name);

            var post = await _postService.GetPostByIdAsync(request.PostId);

            if(!updated)
            {
                return null;
            }

            return post.Adapt<PostResponse>();
        }
    }
}
