using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Contracts.V1.Responses
{
    public class TagResponse
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public Guid PostId { get; set; }
    }
}
