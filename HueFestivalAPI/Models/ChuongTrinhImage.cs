using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("ChuongTrinhImage")]
    public class ChuongTrinhImage
    {
        [Key]
        public int IdImage { get; set; }
        [MaxLength(200)]
        public string PathImage { get; set; }
        public int IdChuongTrinh { get; set; }
        public ChuongTrinh ChuongTrinh { get; set; }
    }
}
