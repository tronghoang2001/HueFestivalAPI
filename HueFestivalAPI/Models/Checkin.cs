using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("Checkin")]
    public class Checkin
    {
        [Key]
        public int IdCheckin { get; set; }
        public DateTime DateCheckin { get; set; }
        public int IdThongTin { get; set; }
        public ThongTinDatVe ThongTinDatVe { get; set; }
    }
}
