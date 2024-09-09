using AutoMapper;
using Demo.DAL.Entities;
using Demo.Pl.ViewModels;

namespace Demo.Pl.Mappers
{
    public class TagProfile: Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, AddTagViewModel>().ReverseMap();
            CreateMap<Tag, EditTagViewModel>().ReverseMap();
        }
    }
}
