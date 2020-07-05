using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //Eliminamos este, porque se creo una extension de PostRepository en PostRepository 
        //con un metodo mas (Buscar Post por id de usuario)
        //private readonly IRepository<Post> _postRepository;
        //Luego de la extension, si se hace uso del la nueva Interface
        private readonly IPostRepository _postRepository; //Nueva Implementacion extendida

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _CommentRepository;

        private readonly SocialMediaContext _context;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
        //Se Elimina para agregar la nueva imprementacion extendida
        //public IRepository<Post> PostRepository => _postRepository ?? new BaseRepository<Post>(_context);
        //Luego de esto ir a IUnitOfWork y reemplazar la firma para PostRepository
        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context); //nuevo approach

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => _CommentRepository ?? new BaseRepository<Comment>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
