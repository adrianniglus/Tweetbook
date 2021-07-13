using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetPostsAsync();
        Task<PostDTO> GetPostByIdAsync(Guid postId);
        Task<bool> CreatePostAsync(Guid postId, string name);
        Task<bool> UpdatePostAsync(Guid postId, UpdatePostRequest request);
        Task<bool> DeletePostAsync(Guid postId);
    }
}
