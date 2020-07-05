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
        //private readonly IPostRepository postRepository;
        //private readonly IUserRepository _userRepository;

        //public PostServices(IPostRepository postRepository, IUserRepository userRepository)
        //{
        //    this.postRepository = postRepository;
        //    this._userRepository = userRepository;
        //}
        private readonly IRespository<Post> postRepository;
        private readonly IRespository<User> _userRepository;
        public PostServices(IRespository<Post> postRepository, IRespository<User> userRepository)
        {
            this.postRepository = postRepository;
            this._userRepository = userRepository;
        }

        public async Task<bool> DeletePost(int id)
        {
            await postRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await postRepository.GetAll();
        }

        public async Task<Post> GetPosts(int id)
        {
            return await postRepository.GetById(id);
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn´t exist");
            }

            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("The commnen conect not allowed");
            }

            await postRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
             await postRepository.Update(post);
            return true;
        }
    }
}
