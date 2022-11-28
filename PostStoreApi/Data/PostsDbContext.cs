using PostStoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PostStoreApi.Data
{
    public class PostsDbContext : IdentityDbContext<ApplicationUser>
    {
        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }  
    }
}
