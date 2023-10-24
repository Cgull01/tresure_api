using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API_tresure.Models
{
    public class User : IdentityUser
    {
    }

    public record PostUser(
        [Required] string Username,
        [Required] [DataType(DataType.EmailAddress)]string Email,
        [Required] [DataType(DataType.Password)] string Password
        );

}
