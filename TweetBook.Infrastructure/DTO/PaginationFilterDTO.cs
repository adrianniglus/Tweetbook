using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetBook.Infrastructure.DTO
{
    public class PaginationFilterDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
