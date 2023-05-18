using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.Account
{
    public class AddChucNangDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
