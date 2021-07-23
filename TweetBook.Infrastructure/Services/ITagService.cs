using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public interface ITagService
    {
        Task<TagDTO> GetTagByIdAsync(Guid tagId);
        Task<List<TagDTO>> GetAllTagsAsync();
        Task<bool> CreateTagAsync(Guid id, Guid postId, string name);
        Task<bool> UpdateTagAsync(Guid tagId, string name);
        Task<bool> DeleteTagAsync(Guid tagId);
        Task<bool> UserOwnsTagAsync(Guid postId, string userId);
    }
}
