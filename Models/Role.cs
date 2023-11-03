
using System.ComponentModel.DataAnnotations;
using tresure_api.Data.Enum;

namespace API_tresure.Models
{
    public class Role
    {
        [Key] public int Id { get; set; }
        [Required] public MemberRoles Name { get; set; }
    }

    public class PostRoleDTO
    {
        public int Id {get;set;}
    }
    public class GetRoleDTO
    {
        public MemberRoles Name {get;set;}
    }
}
