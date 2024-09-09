using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthAppDbContext _dbContext;

        public UserRepository(AuthAppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();

            var superAdminUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == "superadmin@bloggie.com");

            if (superAdminUser != null) 
            {
                users.Remove(superAdminUser);
            }
            return users;
        }
    }
}
