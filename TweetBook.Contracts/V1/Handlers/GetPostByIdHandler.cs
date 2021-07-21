using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Queries;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostResponse>
    {
        private readonly IPostService _postService;

        public GetPostByIdHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PostResponse> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetPostByIdAsync(request.Id);

            if (post == null)
            {
                return null;
            }

            var result = post.Adapt<PostResponse>();
            return result;
        }
    }
}
