using PostStoreApi.Data;
using PostStoreApi.Interfaces;
using PostStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;
using PostStoreApi.DTOs;

namespace PostStoreApi.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostsDbContext _context;

        public PostRepository(PostsDbContext context)
        {
            _context = context;
        }
        public async Task<Post> Add(Post Post)
        {
            await _context.Posts.AddAsync(Post);
            await _context.SaveChangesAsync();
            return Post;
        }

        public async Task<bool> Delete(int id)
        {

            var Post = await _context.Posts.FindAsync(id);
            if (Post == null)
                return false;
            else
            {
                _context.Posts.Remove(Post);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<PostDto>> GetAllPosts()
        {
            return await _context.Posts.ProjectToType<PostDto>().ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            var Post = await _context.Posts.FindAsync(id);
            //if (Post == null)
            //    return false;
            return Post;
        }

        public async Task<Post> Update(int id, Post PostChanegs)
        {
            var Post = await _context.Posts.FindAsync(id);
            if (Post == null)
                return Post;
            else
            {
                Post.Title = PostChanegs.Title;
                await _context.SaveChangesAsync();
                return Post;
            }
        }
    }
}
