using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(SocialMediaContext socialMediaContext)
        {
            this._context = socialMediaContext;
            _entities = _context.Set<T>(); 
        }

        public IEnumerable<T> GetAll()
        {
            //return await _entities.ToListAsync();
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _entities.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        //public async Task Update(T entity)
        public void Update(T entity)
        {
            _entities.Update(entity);
            //await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
