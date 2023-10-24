using System.ComponentModel.DataAnnotations;

namespace API_tresure.Models
{
    public class Project /*: IUserOwnedResource*/
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Project Title Is Required")]
        public string Title { get; set; } = string.Empty;
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Member> Members { get; set; } = new List<Member>();
    }

    public record PostProjectDTO(string title);
    public record PutProjectDTO(int Id, string title);
}