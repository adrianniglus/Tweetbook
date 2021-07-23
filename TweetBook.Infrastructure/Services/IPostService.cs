using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Infrastructure.DTO;
using TweetBook.Infrastructure.Models;

namespace TweetBook.Infrastructure.Services
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetPostsAsync();
        Task<PostDTO> GetPostByIdAsync(Guid postId);
        Task<bool> CreatePostAsync(Guid id, string name, string userId, List<TagModel> tags);
        Task<bool> UpdatePostAsync(Guid postId, string name);
        Task<bool> DeletePostAsync(Guid postId);
        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
    }
}
