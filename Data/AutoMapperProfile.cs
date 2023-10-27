using API_tresure.Models;
using AutoMapper;

namespace tresure_api.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<Project, GetProjectDTO>();
            CreateMap<Project, GetProjectsDTO>();
            CreateMap<Project, CreateProjectDTO>();


            CreateMap<Column, GetColumnDTO>();
            CreateMap<Column, CreateProjectDTO>();
            CreateMap<Member, GetMemberDTO>();

            CreateMap<Card, GetCardDTO>();
        }
    }
}