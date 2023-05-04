using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("DiaDiem")]
    public class DiaDiem
    {
        [Key]
        public int IdDiaDiem { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength (200)]
        public string PathImage { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int TypeData { get; set; }
        public DateTime PostDate { get; set; }
        public int IdAccount { get; set; }
        public int IdSubMenu { get; set; }
        public Account Account { get; set; }
        public DiaDiemSubMenu DiaDiemSubMenu { get; set; }
        public ICollection<ChuongTrinhDetails> ChuongTrinhDetails { get; set; }
    }
}
