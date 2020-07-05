using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostServices
    {
        IEnumerable<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPosts(int id);
        Task InsertPost(Post post);
        Task<bool> DeletePost(int id);
        Task<bool> UpdatePost(Post post);

    }
}