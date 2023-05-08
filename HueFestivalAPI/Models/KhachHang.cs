using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int IdKhachHang { get; set; }
        [Required]
        [MaxLength(50)]
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        [Required]
        [MaxLength(15)]
        public string SoCMND { get; set; }
        [Required]
        [MaxLength(15)]
        public string SoDienThoai { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
