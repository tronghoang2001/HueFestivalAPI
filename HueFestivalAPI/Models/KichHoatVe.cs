using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("KichHoatVe")]
    public class KichHoatVe
    {
        [Key]
        public int IdKichHoat { get; set; }
        public string QRCode { get; set; }
        public DateTime NgayKichHoat { get; set; }
        public int IdThongTin { get; set; }
        public ThongTinDatVe ThongTinDatVe { get; set; }
        public ICollection<Checkin> Checkins { get; set; }
    }
}
