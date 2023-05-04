using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("NhomChuongTrinh")]
    public class NhomChuongTrinh
    {
        [Key]
        public int IdNhom { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ChuongTrinhDetails> ChuongTrinhDetails { get; set; }
    }
}
