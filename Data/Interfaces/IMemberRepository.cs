using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace tresure_api.Data.Interfaces
{
    public interface IMemberRepository
    {
        Task<ICollection<Member>> GetMembers();
        //Task<ICollection<Member>> GetMembersByProjectId(int projectId);

        Task<Member> GetMemberById(int id);
        bool CreateMember(Member member);
        bool UpdateMember(Member member);
        bool DeleteMember(Member member);
        bool Save();

    }
}
