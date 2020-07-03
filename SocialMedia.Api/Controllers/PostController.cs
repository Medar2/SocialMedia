using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //recomendado para API

    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            this._postRepository = postRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            //var posts = new PostRepository().GetPosts();}
            var posts = await _postRepository.GetPosts();
            //AutoMapper
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            //var postsDto = posts.Select(x => new PostDto
            //{
            //    Postid = x.Postid,
            //    Date = x.Date,
            //    Image = x.Image,
            //    UserId = x.UserId

            //});
            return Ok(postsDto);
        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            
            var post = await _postRepository.GetPosts(id);
            var postsDto = _mapper.Map<PostDto>(post);
            //var postsDto = new PostDto
            //{
            //    Postid = post.Postid,
            //    Date = post.Date,
            //    Image = post.Image,
            //    UserId = post.UserId

            //};
            return Ok(postsDto);

        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            //Convertir Dto a entidad de Dominio
            //Mapear
            //var post = new Post
            //{
            //    Date = postDto.Date,
            //    Image = postDto.Image,
            //    UserId = postDto.UserId
            //};

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //AutoMapper
            var post = _mapper.Map<Post>(postDto);

            await _postRepository.InsertPost(post);
            return Ok(postDto);

        }
    }
}
