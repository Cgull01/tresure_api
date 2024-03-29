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

            CreateMap<User, getUserDTO>();


            CreateMap<Column, GetColumnDTO>();
            CreateMap<Column, PostColumnDTO>();
            CreateMap<Column, EditColumnDTO>();
            CreateMap<EditColumnDTO, Column>();

            CreateMap<MemberRole, MemberRoleDTO>();
            CreateMap<Role, GetRoleDTO>();

            CreateMap<Member, GetMemberDTO>();
            CreateMap<Member, PostMemberDTO>();
            CreateMap<Member, AssignedMemberDTO>();
            CreateMap<AssignedMemberDTO, Member>();
            CreateMap<EditMemberDTO, Member>();
            CreateMap<PostMemberDTO, Member>();

            CreateMap<Card, GetCardDTO>();
            CreateMap<Card, PostCardDTO>();
            CreateMap<PostCardDTO, Card>();
            CreateMap<EditCardDTO, Card>();
        }
    }
}