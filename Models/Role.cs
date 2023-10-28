
using System.ComponentModel.DataAnnotations;
using tresure_api.Data.Enum;

namespace API_tresure.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required] public MemberRole Name { get; set; }
    }

    public class PostRoleDTO
    {
        int Id {get;set;}
    }
}
