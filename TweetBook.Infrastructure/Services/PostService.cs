using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Data;
using TweetBook.Infrastructure.DTO;

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
            => await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);


        public async Task<List<PostDTO>> GetPostsAsync()
            => await _dataContext.Posts.ToListAsync();

        public async Task<bool> CreatePostAsync(Guid postId, string name)
        {
            var post = new PostDTO
            {
                Id = postId,
                Name = name
            };

            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;


        }


        public async Task<bool> UpdatePostAsync(Guid postId, UpdatePostRequest postRequest)
        {
            var post = new PostDTO
            {
                Id = postId,
                Name = postRequest.Name
            };

            _dataContext.Posts.Update(post);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);

            if (post == null)
                return false;

            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;

        }

    }
}
