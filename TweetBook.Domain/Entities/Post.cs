using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweetBook.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
