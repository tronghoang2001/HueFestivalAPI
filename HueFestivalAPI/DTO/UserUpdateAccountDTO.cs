using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class UserUpdateAccountDTO
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
