using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Contracts.V1.Commands.Tags
{
    public class DeleteTagCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
