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
        private readonly IRespository<Post> _postRepository;
        private readonly IRespository<User> _userRepository;
        private readonly IRespository<Comment> _CommentRepository;

        private readonly SocialMediaContext _context;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
        public IRespository<Post> PostRepository => _postRepository ?? new BaseRepository<Post>(_context);

        public IRespository<User> UserRepository => new BaseRepository<User>(_context);

        public IRespository<Comment> CommentRepository => new BaseRepository<Comment>(_context);

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
