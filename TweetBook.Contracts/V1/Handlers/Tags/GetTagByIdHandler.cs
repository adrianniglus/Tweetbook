using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Queries.Tags;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Tags
{
    public class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, TagResponse>
    {
        private readonly ITagService _tagService;

        public GetTagByIdHandler(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<TagResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagService.GetTagByIdAsync(request.Id);

            return tag.Adapt<TagResponse>();
        }
    }
}
