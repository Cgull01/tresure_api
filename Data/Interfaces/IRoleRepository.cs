using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using tresure_api.Data.Enum;

namespace tresure_api.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task <ICollection<Role>> GetRoles();
        Task <Role> GetRoleByName(MemberRoles roleName);
        Task <Role> GetRoleById(int id);
    }
}
