using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetPostsAsync();
        Task<PostDTO> GetPostByIdAsync(Guid postId);
        Task<bool> CreatePostAsync(PostDTO postDto);
        Task<bool> UpdatePostAsync(PostDTO postDto);
        Task<bool> DeletePostAsync(Guid postId);
        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
        Task<List<TagDTO>> GetAllTagsAsync();
        Task<TagDTO> GetTagByIdAsync(Guid tagId);
        Task<bool> CreateTagAsync(TagDTO tagDto);
    }
}
