using Demo.DAL.Entities;

namespace Demo.Pl.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
