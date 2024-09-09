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
    public class TagRepository : GenaricRepository<Tag>, ITagRepository
    {
        private readonly AppDbContext _dbContext;

        public TagRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Tags.CountAsync();
        }

        public async Task<IEnumerable<Tag>> SearchByName(string searchQuery)
        {
            return await _dbContext.Tags.Where(E => E.Name.Contains(searchQuery)).ToListAsync();
        }

        public async Task<IEnumerable<Tag>> SortAsc(string sortBy)
        {
            return await _dbContext.Tags.OrderBy(E => E.Name).ToListAsync();
        }

        public async Task<IEnumerable<Tag>> SortDesc(string sortBy)
        {
            return await _dbContext.Tags.OrderByDescending(E => E.Name).ToListAsync();
        }
    }
}
