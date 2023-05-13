using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        [MaxLength(200)]
        public string OldPassword { get; set; }
        [Required]
        [MaxLength(200)]
        public string NewPassword { get; set; }
        [Required]
        [MaxLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
