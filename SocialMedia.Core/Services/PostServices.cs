using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
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
                throw new Exception("User doesn´t exist");
            }

            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("The commnen conect not allowed");
            }

            await _unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
             await _unitOfWork.PostRepository.Update(post);
            return true;
        }
    }
}
