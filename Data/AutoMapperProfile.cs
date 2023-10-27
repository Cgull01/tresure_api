using API_tresure.Models;
using AutoMapper;

namespace tresure_api.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<Project, GetProjectDTO>();
            CreateMap<Column, GetColumnDTO>();
            CreateMap<Member, GetMemberDTO>();
        }
    }
}