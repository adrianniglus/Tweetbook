using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain.Entities;
using TweetBook.Infrastructure.DTO;

namespace TweetBook.Infrastructure.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _dataContext;

        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<TagDTO> GetTagByIdAsync(Guid tagId)
        {
            var tag = await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Id == tagId);

            return tag.Adapt<TagDTO>();
        }

        public async Task<List<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _dataContext.Tags.ToListAsync();

            return tags.Adapt<List<TagDTO>>();
        }
        public async Task<bool> CreateTagAsync(Guid id, Guid postId, string name)
        {
            var tag = new Tag
            {
                Id = id,
                TagName = name,
                PostId = postId
            };


            await _dataContext.Tags.AddAsync(tag);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;


        }

        public async Task<bool> UpdateTagAsync(Guid tagId, string name)
        {
            var tag = await GetEntityTagByIdAsync(tagId);

            tag.TagName = name;


            _dataContext.Tags.Update(tag);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }


        public async Task<bool> DeleteTagAsync(Guid tagId)
        {
            var tagDto = await GetTagByIdAsync(tagId);

            var tag = tagDto.Adapt<Tag>();

            if (tag == null)
                return false;

            _dataContext.Tags.Remove(tag);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UserOwnsTagAsync(Guid postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            if (post.UserId != userId)
            {
                return false;
            }
            return true;
        }

        private async Task<Tag> GetEntityTagByIdAsync(Guid tagId)
        {
            var tag = await _dataContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

            return tag;
        }
    }
}
