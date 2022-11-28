using PostStoreApi.DTOs;
using PostStoreApi.Interfaces;
using PostStoreApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace PostStoreApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    // Specific Type
    // IActionResult
    // ActionResult
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _PostRepository;
        private readonly IStringLocalizer<PostController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public PostController(IPostRepository PostRepository,
            IStringLocalizer<PostController> stringLocalizer,
            IStringLocalizer<SharedResource> _sharedLocalizer)
        {
            _PostRepository = PostRepository;
            _stringLocalizer = stringLocalizer;
            this._sharedLocalizer = _sharedLocalizer;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _PostRepository.GetAllPosts());
        }

        /// <summary>
        /// Get a Post with auther details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns a Post</response>
        /// <response code="404">Post not found in our database</response>
        [HttpGet("{id:int}", Name = "GetPostById")]
        [Produces("application/json")]
        //[Route(Name ="")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var title = _stringLocalizer["Title", "Omran", "Best fullstack developer ever"];
            var sharedTitle = _sharedLocalizer["SharedTitle"];

            var Post = await _PostRepository.GetById(id);
            //return Post;
            if (Post == null)
                return NotFound();
            else
                return Ok(new { Post = Post, title = title, sharedTitle = sharedTitle });
        }

        /// <summary>
        /// Adds New Post 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="Post"> Post </param>
        /// <returns></returns>
        /// <response code="201">Successfully Added newly Post</response>
        [HttpPost("addpost")]
        [Consumes("application/json")]
        //[ProducesResponseType(statusCode: 200, type:typeof(Post))]
        public async Task<IActionResult> Post([FromBody] PostDto Post)
        {
            var PostResult = await _PostRepository.Add(new Post { Title = Post.Title });
            //return Ok(PostResult);
            //return CreatedAtRoute("GetPostById", new { id = PostResult.Id }, PostResult);
            return CreatedAtAction(nameof(Get), new { id = PostResult.Id }, PostResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Post Post)
        {
            var result = await _PostRepository.Update(id, Post);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _PostRepository.Delete(id);
            if (result == true)
                return Ok("Post has been Deleted");
            else
                return BadRequest();
        }
    }
}
