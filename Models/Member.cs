using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace API_tresure.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public required string UserId { get; set; }
        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();

        public List<Card> Cards { get; set; } = new List<Card>();


    }

    public class GetMemberDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Role> Roles { get; set; }
    }
    public class PostMemberDTO
    {
        public int Id { get; set; }
    }
}
