using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.DiaDiem
{
    public class AddDiaDiemSubMenuDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public int TypeData { get; set; }
        public int IdMenu { get; set; }
    }
}
