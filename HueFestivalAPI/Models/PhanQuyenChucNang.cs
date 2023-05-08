using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("PhanQuyenChucNang")]
    public class PhanQuyenChucNang
    {
        public int IdQuyen { get; set; }
        public int IdChucNang { get; set;}
        public Quyen Quyen { get; set; }
        public ChucNang ChucNang { get; set; }
    }
}
