using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_tresure.Models
{
    public class Column
    {

        [Key] public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Card> Cards { get; set; } = new List<Card>();
        [Required(ErrorMessage = "Column Position Is Required")] public required int Position { get; set; }
        [Required(ErrorMessage = "Project Id Is Required")] [ForeignKey("Project")] public int ProjectId { get; set; }
        public Project Project { get; set; }
    }

    public class GetColumnDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<GetCardDTO> Cards { get; set; }
        public int Position { get; set; }
    }

    public class PostColumnDTO
    {
        public int ProjectId {get;set;}
    }

    public class EditColumnDTO
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public int Position {get;set;}
    }
}