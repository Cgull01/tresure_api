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

    public class PostProjectDTO
    {
        public string title { get; set; }
    };
    public class PutProjectDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
    };

    public class GetProjectDTO{
         public string Title { get; set; } = string.Empty;
        public List<GetColumnDTO> Columns { get; set; } = new List<GetColumnDTO>();
        public List<GetMemberDTO> Members { get; set; } = new List<GetMemberDTO>();
    }
}