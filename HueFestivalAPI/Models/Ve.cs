using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HueFestivalAPI.Models
{
    [Table("Ve")]
    public class Ve
    {
        [Key]
        public int IdVe { get; set; }
        public decimal GiaVe { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayPhatHanh { get; set; }
        public int IdDetails { get; set; }
        public ChuongTrinhDetails ChuongTrinhDetails { get; set; }
        public int IdLoaiVe { get; set; }
        public LoaiVe LoaiVe { get; set; }
        [JsonIgnore]
        public ICollection<ThongTinDatVe> ThongTinDatVes { get; set; }
    }
}
