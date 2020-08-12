using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] //recomendado para API
    
    //<NoWarn>$(NoWarn):1591</NoWarn> in .csprj para omitir los Warning:

    public class PostController : ControllerBase
    {
        private readonly IPostServices postServices;

        //private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostServices postServices, IMapper mapper, IUriService uriService)
        {
            this.postServices = postServices;
            //this._postRepository = postRepository;
            this._mapper = mapper;
            this._uriService = uriService;
        }
        //TODO: para poder ver los summary en la documentacion ir a proyecto.csproj y agregar:
          //<PropertyGroup>
          //  <GenerateDocumentationFile>true</GenerateDocumentationFile>
          //</PropertyGroup>


        /// <summary>
        /// Utilizado para traer todos los Post
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK,Type = typeof(ApiResponse<IEnumerable<PostDto>>))] //Tipos de Respuestas
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
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
                HasPreviuosPage = posts.HasPreviousPage,
                //NexPageUrl = _uriService.GetPostPaginationUri(filters,"/api/Post").ToString()
                NexPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PreviuosPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
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
