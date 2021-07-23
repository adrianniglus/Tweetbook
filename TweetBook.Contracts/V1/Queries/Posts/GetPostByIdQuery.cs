using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Contracts.V1.Queries.Posts
{
    public class GetPostByIdQuery : IRequest<PostResponse>
    {
        public Guid Id { get; }

        public GetPostByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
