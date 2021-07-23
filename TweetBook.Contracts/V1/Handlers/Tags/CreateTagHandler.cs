using Mapster;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Tags;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Tags
{
    public class CreateTagHandler : IRequestHandler<CreateTagMergedCommand, TagResponse>
    {
        private readonly ITagService _tagService;

        public CreateTagHandler(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<TagResponse> Handle(CreateTagMergedCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var created = await _tagService.CreateTagAsync(id, request.PostId,request.TagName);

            if(!created)
            {
                return null;
            }
            var tag = await _tagService.GetTagByIdAsync(id);

            return tag.Adapt<TagResponse>();

        }
    }
}
