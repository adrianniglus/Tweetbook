using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Infrastructure.DTO;
using Mapster;
using TweetBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TweetBook.Infrastructure.Models;

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
            var post = await _dataContext.Posts.AsNoTracking().Include(x => x.Tags).SingleOrDefaultAsync(x => x.Id == postId);

            return post.Adapt<PostDTO>();

        }


        public async Task<List<PostDTO>> GetPostsAsync()
        {
            var posts = await _dataContext.Posts.AsNoTracking().Include(x => x.Tags).ToListAsync();
            return posts.Adapt<List<PostDTO>>();

        }
        public async Task<bool> CreatePostAsync(Guid id,string name, string userId, List<TagModel> tags)
        {
            var post = new Post
            {
                Id = id,
                Name = name,
                UserId = userId,
                Tags = tags.Adapt<List<Tag>>()
            };
            

            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;


        }


        public async Task<bool> UpdatePostAsync(Guid postId, string name)
        {

            var post = await GetEntityPostByIdAsync(postId);

            post.Name = name;
                       

            _dataContext.Posts.Update(post);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {

            var post = await GetEntityPostByIdAsync(postId);

            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;

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

        private async Task<Post> GetEntityPostByIdAsync(Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Tags).SingleOrDefaultAsync(x => x.Id == postId);

            return post;

        }
    }
}
