using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        //Task<IEnumerable<Post>> GetPosts();
        //Task<Post> GetPosts(int id);
        //Task InsertPost(Post post);
        //Task<bool> DeletePost(int id);
        //Task<bool> UpdatePost(Post post);

        Task<IEnumerable<Post>> GetPostsByUser(int userId);

    }
}
