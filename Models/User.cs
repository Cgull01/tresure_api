using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API_tresure.Models
{
    public class User : IdentityUser
    {
    }

    public class PostLoginUser
    {
        [Required] public string Username { get; set; }
        [Required][DataType(DataType.EmailAddress)] public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
    }

    public class getLoginUser
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
    public class RegisterUser
    {
        [Required] public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required] public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required] public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Required] public string RepeatPassword { get; set; }
    }

}
