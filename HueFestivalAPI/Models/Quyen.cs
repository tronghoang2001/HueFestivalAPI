using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("Quyen")]
    public class Quyen
    {
        [Key]
        public int IdQuyen { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<PhanQuyenChucNang> PhanQuyenChucNangs { get; set; }
    }
}
