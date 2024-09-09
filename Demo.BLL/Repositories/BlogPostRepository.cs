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
    public class BlogPostRepository : GenaricRepository<BlogPost>, IBlogPostRepository
    {
        private readonly AppDbContext dbContext;

        public BlogPostRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext=dbContext;
        }
        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            return await dbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }
        public async Task<BlogPost> GetById(Guid id)
        {
            //return _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefault();
            return await dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(b => b.Tags).FirstOrDefaultAsync(b => b.UrlHandle == urlHandle);
        }

    }
}
