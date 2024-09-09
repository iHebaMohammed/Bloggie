using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface ITagRepository : IGenaricRepository<Tag>
    {
        Task<IEnumerable<Tag>> SearchByName(string searchQuery);
        Task<IEnumerable<Tag>> SortAsc(string sortBy);
        Task<IEnumerable<Tag>> SortDesc(string sortBy);
        Task<int> CountAsync();
    }
}
