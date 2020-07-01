//using Microsoft.EntityFrameworkCore;
//using SocialMedia.Core.Entities;
//using SocialMedia.Core.Interfaces;
//using SocialMedia.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SocialMedia.Infrastructure.Repositories
//{
//    public class PostMongoRepository : IPostRepository
//    {
//        private readonly SocialMediaContext socialMediaContext;

//        public PostMongoRepository(SocialMediaContext socialMediaContext)
//        {
//            this.socialMediaContext = socialMediaContext;
//        }
//        public async Task<IEnumerable<Post>> GetPosts()
//        {
//            //var posts = Enumerable.Range(1, 10).Select(x => new Post
//            //{
//            //    PostId = x,
//            //    Description = $"Description MONGO {x}",
//            //    Date = DateTime.Now,
//            //    Image = $"https://misapis.com{x}",
//            //    UserId = x * 2

//            //});
//            //await Task.Delay(10);
//            var posts = await socialMediaContext.Posts.ToArrayAsync();
//            return posts;
//        }
//    }
//}
