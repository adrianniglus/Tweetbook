using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Responses;

namespace TweetBook.Contracts.V1.Queries.Tags
{
    public class GetTagByIdQuery : IRequest<TagResponse>
    {
        public Guid Id { get; }

        public GetTagByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
