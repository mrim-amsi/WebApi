using PostStoreApi.DTOs;
using PostStoreApi.Interfaces;
using PostStoreApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using PostStoreApi.Migrations;
using Microsoft.EntityFrameworkCore;

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
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostController(IPostRepository PostRepository,
            IStringLocalizer<PostController> stringLocalizer,
            IStringLocalizer<SharedResource> _sharedLocalizer,
            IWebHostEnvironment hostEnvironment)
        {
            _PostRepository = PostRepository;
            _stringLocalizer = stringLocalizer;
            this._sharedLocalizer = _sharedLocalizer;
            _hostEnvironment = hostEnvironment;
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

        [HttpPost]
        [Route("")]
        [RequestSizeLimit(5 * 1024 * 1024)]
        //[ProducesResponseType(statusCode: 200, type:typeof(Post))]
        public async Task<IActionResult> Post([FromForm] PostDto Post)
        {


            var uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(Post.Image.FileName);
            // ->0f8fad5b-d9cb-469f-a165-70867728950e.jpg

            var filePath = Path.Combine(uploadFolder, uniqueName);

            //webserver/Images/0f8fad5b-d9cb-469f-a165-70867728950e.jpg
            Post.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            var PostResult = await _PostRepository.Add(new Post
            {
                Title = Post.Title,
                Description = Post.Description,
                UserId = Post.UserId,
                Imagepath = uniqueName,
                Ts = DateTime.Now,
                Published = true
            });

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
