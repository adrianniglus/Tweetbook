using MediatR;
using System;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Contracts.V1.Commands.Tags
{
    public class CreateTagMergedCommand : IRequest<TagResponse>
    {
        public Guid PostId { get; set; }
        public string TagName { get; set; }
    }
}
