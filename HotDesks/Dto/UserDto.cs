using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotDesks.Api.Dto
{
    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "Your password must contain between 8 and 16 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserDto : LoginUserDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public ICollection<string> Roles { get; set; }

    }    
}
