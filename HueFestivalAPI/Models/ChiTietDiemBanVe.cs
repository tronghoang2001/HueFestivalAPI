using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("ChiTietDiemBanVe")]
    public class ChiTietDiemBanVe
    {
        [Key]
        public int IdChiTietDiemBanVe { get; set; }
        public int SoLuong { get; set; }
        public int IdDiemBanVe { get; set; }
        public DiemBanVe DiemBanVe { get; set; }
        public int IdVe { get; set; }
        public Ve Ve { get; set; }
    }
}
