using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddChucNangDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
