using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.Models
{
    [Table("DoanChuongTrinh")]
    public class DoanChuongTrinh
    {
        [Key]
        public int IdDoan { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ChuongTrinhDetails>? ChuongTrinhDetails { get; set; }
    }
}
