using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        //Eliminado por la implementacion en UnitOfWork de la nueva extencion para PostRepository
        //IRepository<Post> PostRepository { get; }
        IPostRepository PostRepository { get; } //Nueva Implementacion despues de la extensión


        IRepository<User> UserRepository { get; }

        IRepository<Comment> CommentRepository { get; }

        ISecurityRepository SecurityRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
