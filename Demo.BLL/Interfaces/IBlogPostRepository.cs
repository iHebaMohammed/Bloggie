using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IBlogPostRepository : IGenaricRepository<BlogPost>
    {
        public Task<BlogPost?> GetByUrlHandle(string urlHandle);
    }
}
