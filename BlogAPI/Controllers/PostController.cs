using BlogDataLibrary.Data;
using BlogDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ALL endpoints in this controller now require token
    public class PostController : ControllerBase
    {
        private readonly ISqlData _db;

        public PostController(ISqlData db)
        {
            _db = db;
        }

        [HttpGet("list")]
        public IActionResult ListPosts()
        {
            var posts = _db.ListPosts();
            return Ok(posts);
        }

        [HttpGet("detail/{id}")]
        public IActionResult ShowPostDetails(int id)
        {
            var post = _db.ShowPostDetails(id);

            if (post != null)
            {
                return Ok(post);
            }

            return NotFound("Post not found");
        }

        [HttpPost("add")]
        public IActionResult AddPost([FromBody] PostForm postForm)
        {
            int userId = GetCurrentUserId();
            _db.AddPost(userId, postForm.Title, postForm.Body);
            return Ok("Post created");
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
