using Mapster;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Queries.Tags;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Tags
{
    public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, List<TagResponse>>
    {
        private readonly ITagService _tagService;

        public GetAllTagsHandler(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<List<TagResponse>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {

            var tags = await _tagService.GetAllTagsAsync();

            return tags.Adapt<List<TagResponse>>();
            
        }
    }
}
