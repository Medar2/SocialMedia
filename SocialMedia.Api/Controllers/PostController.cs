using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //recomendado para API

    public class PostController : ControllerBase
    {
        private readonly IPostServices postServices;

        //private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostServices postServices, IMapper mapper)
        {
            this.postServices = postServices;
            //this._postRepository = postRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)] //Tipos de Respuestas
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {
            //var posts = new PostRepository().GetPosts();}
            var posts = postServices.GetPosts(filters);
            //var postDto = _mapper.Map<PostDto>(posts);


            //AutoMapper
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var metadata = new Metadata
            {
                TotalPage = posts.TotalPages,
                PageSize = posts.PageSize,
                TotalCount = posts.TotalCount,
                CurrentPage = posts.CurrentPage,
                HasNextPage = posts.HasNextPage,
                HasPreviuosPage = posts.HasPreviousPage

            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta = metadata
            };

            ////Implementar Paginacion ojo ** se dejo igual porque el PagedList hereda de un list
            //var postsDto = _mapper.Map<PagedList<PostDto>>(posts);
            //var response = new ApiResponse<PagedList<PostDto>>(postsDto);
            //-------------------------------------------------------------------------------------

            //var postsDto = posts.Select(x => new PostDto
            //{
            //    Postid = x.Postid,
            //    Date = x.Date,
            //    Image = x.Image,
            //    UserId = x.UserId

            //});
            //var metadata = new
            //{
            //    posts.TotalPages,
            //    posts.PageSize,
            //    posts.TotalCount,
            //    posts.CurrentPage,
            //    posts.HasNextPage,
            //    posts.HasPreviousPage
            //};
          
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); ///primera opcion de paginacion
            return Ok(response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {

            var post = await postServices.GetPosts(id);
            var postsDto = _mapper.Map<PostDto>(post);            
            var response = new ApiResponse<PostDto>(postsDto);
            //var postsDto = new PostDto
            //{
            //    Postid = post.Postid,
            //    Date = post.Date,
            //    Image = post.Image,
            //    UserId = post.UserId

            //};
            return Ok(response);

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

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //AutoMapper
            var post = _mapper.Map<Post>(postDto);

            await postServices.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);

            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {

            //AutoMapper
            var post = _mapper.Map<Post>(postDto);

            post.Id = id;
            var result = await postServices.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await postServices.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
