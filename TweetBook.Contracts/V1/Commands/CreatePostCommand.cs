using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Contracts.V1.Commands
{
    public class CreatePostCommand : IRequest<PostResponse>
    {
        public string Name { get; set; }
        public List<CreateTagCommand> Tags { get; set; }
    }
}
