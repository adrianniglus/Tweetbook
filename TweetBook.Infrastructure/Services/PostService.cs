using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Infrastructure.DTO;
using Mapster;
using TweetBook.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace TweetBook.Infrastructure.Services
{
    public class PostService : IPostService
    {

        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<PostDTO> GetPostByIdAsync(Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Tags).SingleOrDefaultAsync(x => x.Id == postId);

            return post.Adapt<PostDTO>();

        }


        public async Task<List<PostDTO>> GetPostsAsync()
        {
            var posts = await _dataContext.Posts.Include(x => x.Tags).ToListAsync();


            return posts.Adapt<List<PostDTO>>();
        }
        public async Task<bool> CreatePostAsync(PostDTO postDto)
        {
            var post = postDto.Adapt<Post>();
            

            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;


        }


        public async Task<bool> UpdatePostAsync(PostDTO postDto)
        {
            var post = postDto.Adapt<Post>();
            

            _dataContext.Posts.Update(post);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var postDto = await GetPostByIdAsync(postId);

            var post = postDto.Adapt<Post>();

            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;

        }

        public async Task<List<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _dataContext.Tags.ToListAsync();

            return tags.Adapt<List<TagDTO>>();
        }


        public async Task<bool> CreateTagAsync(TagDTO tagDto)
        {
            var tag = tagDto.Adapt<Tag>();


            await _dataContext.Tags.AddAsync(tag);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;


        }
        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if(post == null)
            {
                return false;
            }

            if(post.UserId != userId)
            {
                return false;
            }
            return true;
        }

        public async Task<TagDTO> GetTagByIdAsync(Guid tagId)
        {
            var tag = await _dataContext.Tags.SingleOrDefaultAsync(x => x.Id == tagId);

            return tag.Adapt<TagDTO>();
        }
    }
}
