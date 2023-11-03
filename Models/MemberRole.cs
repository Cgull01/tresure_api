using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace API_tresure.Models
{
    public class MemberRole
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        // JsonIgnore to avoid object cycle
        [JsonIgnore] public Member Member { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
    public class MemberRoleDTO
    {
        public GetRoleDTO Role { get; set; }
    }


}
