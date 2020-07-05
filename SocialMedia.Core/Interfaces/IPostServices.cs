﻿using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostServices
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPosts(int id);
        Task InsertPost(Post post);
        Task<bool> DeletePost(int id);
        Task<bool> UpdatePost(Post post);

    }
}