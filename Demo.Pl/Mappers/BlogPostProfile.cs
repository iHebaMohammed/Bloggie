using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;

namespace Demo.Pl.Mappers
{
    public class BlogPostProfile: Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, AddBlogPostViewModel>().ReverseMap();
            CreateMap<BlogPost, EditBlogPostViewModel>().ReverseMap();
        }
    }
}
