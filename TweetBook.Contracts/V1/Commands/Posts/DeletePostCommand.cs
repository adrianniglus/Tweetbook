using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Contracts.V1.Commands.Posts
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid postId { get; set; }
    }
}
