﻿using System.ComponentModel.DataAnnotations;

namespace API_tresure.Models
{
    public class Project /*: IUserOwnedResource*/
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Project Title Is Required")]
        public string Title { get; set; } = string.Empty;
        public List<Column> Columns { get; set; }
        public List<Member> Members { get; set; }
    }

    public class CreateProjectDTO
    {
        public string Title {get;set;}
    };
    public class EditProjectDTO
    {
        public int Id {get;set;}
        public string Title {get;set;}
    };

    public class GetProjectDTO
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public List<GetColumnDTO> Columns {get;set;}
        public List<GetMemberDTO> Members {get;set;}
    }
    public class GetProjectsDTO
    {
        public int Id {get;set;}
        public string Title {get;set;}
    }
    // public class PutProjectDTO
    // {
    //     public int Id { get; set; }
    //     public string title { get; set; }
    // };

    // public class GetProjectDTO{
    //     public int Id { get; set; }
    //     public string Title { get; set; } = string.Empty;
    //     public List<GetColumnDTO> Columns { get; set; } = new List<GetColumnDTO>();
    //     public List<GetMemberDTO> Members { get; set; } = new List<GetMemberDTO>();
    // }
}