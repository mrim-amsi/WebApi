using PostStoreApi.DTOs;
using PostStoreApi.Models;

namespace PostStoreApi.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostDto>> GetAllPosts();
        Task<Post> GetById(int id);
        Task<Post> Add(Post Post);
        Task<Post> Update(int id, Post PostChanegs);
        Task<bool> Delete(int id);


    }
}
