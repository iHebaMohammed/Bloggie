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
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogPostCommentRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<int> Add(BlogPostComment blogPostComment)
        {
            await _dbContext.Comments.AddAsync(blogPostComment);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllCommnetByBlogPostId(Guid blogPostId)
        {
            return await _dbContext.Comments.Where(c => c.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
