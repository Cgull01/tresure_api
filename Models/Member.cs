using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace API_tresure.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required] public required string UserId { get; set; }
        [Required] public required int ProjectId { get; set; }

        [ForeignKey("ProjectId")] public Project Project { get; set; }
        [ForeignKey("UserId")] public User User { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();

        public List<Card> Cards { get; set; } = new List<Card>();


    }
}
