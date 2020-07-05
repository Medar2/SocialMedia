using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IRespository<Post> PostRepository { get; }
        IRespository<User> UserRepository { get; }

        IRespository<Comment> CommentRepository { get; }
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
