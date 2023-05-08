using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int IdHoaDon { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayMua { get; set; }
        public int IdKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public int IdChiTietDiemBanVe { get; set; }
        public ChiTietDiemBanVe ChiTietDiemBanVe { get; set; }
        public ICollection<Checkin> Checkins { get; set; }
    }
}
