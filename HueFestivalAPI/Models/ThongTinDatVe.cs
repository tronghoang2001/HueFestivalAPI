using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("ThongTinDatVe")]
    public class ThongTinDatVe
    {
        [Key]
        public int IdThongTin { get; set; }
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
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayDat { get; set; }
        public Boolean TinhTrangThanhToan { get; set; }
        public int IdVe { get; set; }
        public Ve Ve { get; set; }
        public ICollection<Checkin> Checkins { get; set; }
    }
}
