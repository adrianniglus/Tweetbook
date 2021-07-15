using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Infrastructure.DTO
{
    public class TagDTO
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public Guid PostId { get; set; }

    }
}
