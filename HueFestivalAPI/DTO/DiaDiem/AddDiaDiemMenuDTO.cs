using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.DiaDiem
{
    public class AddDiaDiemMenuDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string PathIcon { get; set; }
        public int TypeData { get; set; }
    }
}
