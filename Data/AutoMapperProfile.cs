using API_tresure.Models;
using AutoMapper;

namespace tresure_api.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<Project, GetProjectDTO>();
            CreateMap<Project, GetProjectsDTO>();
            CreateMap<Project, EditProjectDTO>();
            CreateMap<Project, PostProjectDTO>();


            CreateMap<Column, GetColumnDTO>();
            CreateMap<Column, PostCardDTO>();

            CreateMap<Member, GetMemberDTO>();
            CreateMap<Member, PostMemberDTO>();
            CreateMap<PostMemberDTO, Member>();

            CreateMap<Card, GetCardDTO>();
            CreateMap<Card, PostCardDTO>();
            CreateMap<PostCardDTO, Card>();
            CreateMap<EditCardDTO, Card>();
        }
    }
}