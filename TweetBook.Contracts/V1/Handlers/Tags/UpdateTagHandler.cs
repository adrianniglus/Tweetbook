using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Tags;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Tags
{
    public class UpdateTagHandler : IRequestHandler<UpdateTagMergedCommand, TagResponse>
    {
        private readonly ITagService _tagService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTagHandler(ITagService tagService, IHttpContextAccessor httpContextAccessor)
        {
            _tagService = tagService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TagResponse> Handle(UpdateTagMergedCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var tag = await _tagService.GetTagByIdAsync(request.Id);

            var userOwnsPost = await _tagService.UserOwnsTagAsync(tag.PostId, userId);

            if (!userOwnsPost)
            {
                return null;
            }

            var updated = await _tagService.UpdateTagAsync(tag.Id, request.Name);

            var updatedTag = await _tagService.GetTagByIdAsync(tag.Id);

            if (!updated)
            {
                return null;
            }

            return updatedTag.Adapt<TagResponse>();
        }
    }
}
