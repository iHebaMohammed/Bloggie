using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class BlogPostComment
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
