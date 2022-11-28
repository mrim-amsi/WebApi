using PostStoreApi.DTOs;
using PostStoreApi.Interfaces;
using PostStoreApi.Models;

namespace PostStoreApi.Repositories
{
    public class MockPostRepository : IPostRepository
    {
        public static List<Post> _Posts { get; set; }

        public MockPostRepository()
        {
            _Posts = new List<Post>() {
                new Post() { Id = 1, Title ="Asp.net core with angular"},
                new Post() { Id = 2, Title ="Web Development"},
            };
        }
        public Task<Post> Add(Post Post)
        {
            _Posts.Add(Post);
            return Task.FromResult(Post);
        }

        public Task<bool> Delete(int id)
        {
            var Post = _Posts.FirstOrDefault(x => x.Id == id);
            if (Post != null)
            {
                _Posts.Remove(Post);
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);

        }

        public Task<List<PostDto>> GetAllPosts()
        {
            return Task.FromResult(_Posts.Select(x => new PostDto { Title = x.Title }).ToList());
        }

        public Task<Post> GetById(int id)
        {
            var Post = _Posts.FirstOrDefault(x => x.Id == id);
            //if (Post == null)
            //else
            return Task.FromResult(Post);
        }

        public Task<Post> Update(int id, Post PostChanegs)
        {
            var Post = _Posts.FirstOrDefault(x => x.Id == id);
            if (Post != null)
            {
                Post.Title = PostChanegs.Title;
                return Task.FromResult(Post);
            }
            else
                throw new Exception("Post Not found");
        }
    }
}
