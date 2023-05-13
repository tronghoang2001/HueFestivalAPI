using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HueFestivalAPI.Models
{
    [Table("ChucNang")]
    public class ChucNang
    {
        [Key]
        public int IdChucNang { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<PhanQuyenChucNang> PhanQuyenChucNangs { get; set; }
    }
}
