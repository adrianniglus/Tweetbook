using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Tags;
using TweetBook.Infrastructure.Services;

namespace TweetBook.Contracts.V1.Handlers.Tags
{
    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand, bool>
    {
        private readonly ITagService _tagService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteTagHandler(ITagService tagService, IHttpContextAccessor httpContextAccessor)
        {
            _tagService = tagService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagService.GetTagByIdAsync(request.Id);

            string userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var userOwnsPost = await _tagService.UserOwnsTagAsync(tag.PostId, userId);

            if (!userOwnsPost)
            {
                return false;
            }

            var deleted = await _tagService.DeleteTagAsync(tag.Id);

            return deleted;
        }
    }
}
