using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_tresure.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        [Column(TypeName = "jsonb")] public string? Tags { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public List<Member> AssignedMembers { get; set; } = new List<Member>();
        [Required][ForeignKey("Column")] public required int ColumnId { get; set; }
        public Column? Column { get; set; }
    }

    public class GetCardDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public string? Tags { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public List<PostMemberDTO>? AssignedMembers { get; set; }
    }

    public class PostCardDTO
    {
        public string? Title { get; set; }
        public string? Details { get; set; }
        public string? Tags { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public List<PostMemberDTO>? AssignedMembers { get; set; } = new List<PostMemberDTO>();
        public int ColumnId { get; set; }
    }
    public class EditCardDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public string? Tags { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public List<EditMemberDTO>? AssignedMembers { get; set; } = new List<EditMemberDTO>();
        public int ColumnId { get; set; }
    }
}