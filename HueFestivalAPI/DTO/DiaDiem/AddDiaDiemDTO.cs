using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.DiaDiem
{
    public class AddDiaDiemDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int TypeData { get; set; }
        public DateTime PostDate { get; set; }
        public int IdAccount { get; set; }
        public int IdSubMenu { get; set; }
    }
}
