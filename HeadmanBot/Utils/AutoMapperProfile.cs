using AutoMapper;
using HeadmanBot.Data.Entities;
using HeadmanBot.Data.Models;

namespace HeadmanBot.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Group, GroupModel>();
            CreateMap<GroupModel, Group>();
        }
    }
}