using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    //public class PostRepository : IPostRepository
    //En este punto se extiende el PosRepository para agregarle mas metodos, o logica de negocio.
    //en la BaseRepository se puso como protected la variable DbSet<T> _entities para poder acceder
    //desde una extencion como esta y hacer uso solo a la entity deseada, en este caso Post
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        //private readonly SocialMediaContext socialMediaContext;

        //public PostRepository(SocialMediaContext socialMediaContext)
        //{
        //    this.socialMediaContext = socialMediaContext;
        //}

        //public async Task<IEnumerable<Post>> GetPosts()
        //{
        //    //var posts = Enumerable.Range(1, 10).Select(x => new Post
        //    //{
        //    //    PostId = x,
        //    //    Description = $"Description{x}",
        //    //    Date = DateTime.Now,
        //    //    Image = $"https://misapis.com{x}",
        //    //    UserId = x * 2

        //    //});
        //    //await Task.Delay(10);
        //    var posts = await socialMediaContext.Posts.ToArrayAsync();
        //    return posts;
        //}
        //public async Task<Post> GetPosts(int id)
        //{

        //    var posts = await socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
        //    return posts;
        //}
        //public async Task InsertPost(Post post)
        //{
        //    socialMediaContext.Posts.Add(post);
        //   await socialMediaContext.SaveChangesAsync();
        //}
        //public async Task<bool>UpdatePost(Post post)
        //{
        //    var currentPost = await GetPosts(post.Id);
        //    currentPost.Date = post.Date;
        //    currentPost.Description = post.Description;
        //    currentPost.Image = post.Image;
        //    //currentPost.UserId = post.UserId;
        //    int row = await socialMediaContext.SaveChangesAsync();

        //    return row > 0;
        //}

        //public async Task<bool> DeletePost(int id)
        //{
        //    var currentPost = await GetPosts(id);
        //    socialMediaContext.Posts.Remove(currentPost);

        //    int row = await socialMediaContext.SaveChangesAsync();

        //    return row > 0;
        //}
        public PostRepository(SocialMediaContext context) :base(context){}

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
