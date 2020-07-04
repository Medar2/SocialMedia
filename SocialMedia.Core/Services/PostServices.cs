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
        private readonly IPostRepository postRepository;
        private readonly IUserRepository _userRepository;

        public PostServices(IPostRepository postRepository,IUserRepository userRepository)
        {
            this.postRepository = postRepository;
            this._userRepository = userRepository;
        }

        public async Task<bool> DeletePost(int id)
        {
            return await postRepository.DeletePost(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await postRepository.GetPosts();
        }

        public async Task<Post> GetPosts(int id)
        {
            return await postRepository.GetPosts(id);
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn´t exist");
            }

            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("The commnen conect not allowed");
            }

            await postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await postRepository.UpdatePost(post);
        }
    }
}
