using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddChuongTrinhDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public int TypeInOff { get; set; }
        public decimal Price { get; set; }
        public int TypeProgram { get; set; }
        public int Arrange { get; set; }
    }
}
