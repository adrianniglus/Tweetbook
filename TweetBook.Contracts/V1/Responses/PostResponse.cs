using System;
using System.Collections.Generic;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Contracts.V1.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
