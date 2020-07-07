using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SocialMedia.Core.Services
{
    public class PostServices : IPostServices
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IPostRepository postRepository;
        //private readonly IUserRepository _userRepository;

        //public PostServices(IPostRepository postRepository, IUserRepository userRepository)
        //{
        //    this.postRepository = postRepository;
        //    this._userRepository = userRepository;
        //}
        //private readonly IRespository<Post> postRepository;
        //private readonly IRespository<User> _userRepository;
        //public PostServices(IRespository<Post> postRepository, IRespository<User> userRepository)
        //{
        //    this.postRepository = postRepository;
        //    this._userRepository = userRepository;
        //}

        public PostServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

        //public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? 1 : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? 20 : filters.PageSize;

            var posts = _unitOfWork.PostRepository.GetAll();
            if (filters.UserId != null)
            {
                posts = posts.Where(p => p.UserId == filters.UserId); 
            }
            if (filters.Date != null)
            {
                posts = posts.Where(p => p.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(p => p.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            //Implementar Paginacion
            var pagePosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagePosts;
        }

        public async Task<Post> GetPosts(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinesException("User doesn´t exist");
            }

            if (post.Description.Contains("Sexo"))
            {
                throw new BusinesException("The commnen conect not allowed");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);

            if (userPost.Count() < 10 )
            {
                var lasPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();

                if ((DateTime.Now - lasPost.Date).TotalDays < 7)
                {
                    throw new BusinesException("You are not able to publish the post");
                }

            }
            await _unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
             _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
