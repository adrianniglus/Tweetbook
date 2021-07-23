using MediatR;
using System.Collections.Generic;
using TweetBook.Contracts.V1.Commands.Tags;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Contracts.V1.Commands.Posts
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public string Name { get; set; }
        public List<CreateTagCommand> Tags { get; set; }
    }
}
