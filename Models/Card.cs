using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_tresure.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public string? Tags { get; set; }
        public DateTimeOffset? DueDate { get; set; }

        [Required(ErrorMessage = "Creation Date Is Required")]
        public required DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? CompletionDate { get; set; }

        public List<Member> AssignedMembers { get; set; } = new List<Member>();

        [Required]
        [ForeignKey("Column")]
        public required int ColumnId { get; set; }

        public Column? Column { get; set; }
    }

    public class GetCardDTO{
        public int Id{get;set;}
        public string Title{get;set;}
        public string Details{get;set;}
        public string Tags{get;set;}
        public DateTimeOffset DueDate{get;set;}
        public DateTimeOffset CreationDate{get;set;}
        public DateTimeOffset CompletionDate{get;set;}
        public List<GetMemberDTO> AssignedMembers{get;set;}
    }

    public class PostCardDTO{
        public string Title{get;set;}
        public string Details{get;set;}
        public string Tags{get;set;}
        public DateTimeOffset DueDate{get;set;}
        public DateTimeOffset CreationDate{get;set;}
        public DateTimeOffset CompletionDate{get;set;}
        public List<PostMemberDTO> AssignedMembers{get;set;}
        public int ColumnId {get;set;}
    }
    public class EditCardDTO{
        public int Id {get;set;}
        public string Title{get;set;}
        public string Details{get;set;}
        public string Tags{get;set;}
        public DateTimeOffset DueDate{get;set;}
        public DateTimeOffset CreationDate{get;set;}
        public DateTimeOffset CompletionDate{get;set;}
        public List<PostMemberDTO> AssignedMembers{get;set;}
        public int ColumnId {get;set;}
    }
}