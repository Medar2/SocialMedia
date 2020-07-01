using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            //var posts = new PostRepository().GetPosts();}
            var posts = await _postRepository.GetPosts();
        return Ok(posts);
        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            
            var posts = await _postRepository.GetPosts(id);
            return Ok(posts);

        }
    }
}
