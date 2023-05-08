using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("LoaiVe")]
    public class LoaiVe
    {
        [Key]
        public int IdLoaiVe { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<Ve> Ves { get; set; }
    }
}
