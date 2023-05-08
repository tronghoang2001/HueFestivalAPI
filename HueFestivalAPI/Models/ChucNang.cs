using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("ChucNang")]
    public class ChucNang
    {
        [Key]
        public int IdChucNang { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<PhanQuyenChucNang> PhanQuyenChucNangs { get; set; }
    }
}
