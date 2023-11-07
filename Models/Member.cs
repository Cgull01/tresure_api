using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using tresure_api.Data.Enum;

namespace API_tresure.Models
{
    public class Member
    {
        [Key] public int Id { get; set; }
        [Required][ForeignKey("User")] public required string UserId { get; set; }
        [Required][ForeignKey("Project")] public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
        public List<MemberRole> Roles { get; set; } = new List<MemberRole>();
        public List<Card> Cards { get; set; } = new List<Card>();

        public Member(){
            Roles.Add(new MemberRole {Role = new Role {Name = MemberRoles.Member}});
        }
    }

    public class GetMemberDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<MemberRoleDTO> Roles { get; set; }
    }
    public class PostMemberDTO
    {
        public int Id {get;set;}
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }

    public class EditMemberDTO
    {
        public int Id { get; set; }
        public List<MemberRole> Roles { get; set; }
    }
}
