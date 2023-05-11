using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        [MaxLength(255)]
        public string email { get; set; }
        [Required]
        [MaxLength(200)]
        public string password { get; set; }
    }
}
