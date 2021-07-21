using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Infrastructure.Models
{
    public class TagModel
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public Guid PostId { get; set; }
    }
}
