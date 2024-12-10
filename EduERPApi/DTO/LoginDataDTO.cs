using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class LoginDataDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
