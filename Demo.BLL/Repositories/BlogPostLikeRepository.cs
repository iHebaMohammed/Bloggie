using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogPostLikeRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddLikeForBlogPost(BlogPostLike blogPostLike)
        {
            await _dbContext.Likes.AddAsync(blogPostLike);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await _dbContext.Likes.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _dbContext.Likes.CountAsync(l => l.BlogPostId == blogPostId);
        }
    }
}
