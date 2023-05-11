using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("ChuongTrinhDetails")]
    public class ChuongTrinhDetails
    {
        [Key]
        public int IdDetails { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdChuongTrinh { get; set; }
        public int IdDiaDiem { get; set; }
        public int IdNhom { get; set; }
        public int? IdDoan { get; set; }
        public ChuongTrinh ChuongTrinh { get; set; }
        public DiaDiem DiaDiem { get; set; }
        public NhomChuongTrinh NhomChuongTrinh { get; set; }
        public DoanChuongTrinh? DoanChuongTrinh { get; set; }
        public ICollection<Ve> Ves { get; set; }
    }
}
