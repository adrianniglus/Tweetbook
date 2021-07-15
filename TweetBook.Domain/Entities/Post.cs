using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweetBook.Domain.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public List<Tag> Tags { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

    }
}
