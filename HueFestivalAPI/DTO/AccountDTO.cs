using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AccountDTO
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required, EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
