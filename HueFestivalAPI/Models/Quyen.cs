using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore] 
        public ICollection<Account>? Accounts { get; set; }
        [JsonIgnore]
        public ICollection<PhanQuyenChucNang> PhanQuyenChucNangs { get; set; }
    }
}
