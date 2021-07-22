using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletePostHandler(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var userOwnsPost = await _postService.UserOwnsPostAsync(request.postId, userId);

            if(!userOwnsPost)
            {
                return false;
            }

            var deleted = await _postService.DeletePostAsync(request.postId);

            return deleted;
        }
    }
}
