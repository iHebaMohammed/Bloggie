using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IBlogPostCommentRepository
    {
        Task<int> Add(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetAllCommnetByBlogPostId(Guid blogPostId);
    }
}
