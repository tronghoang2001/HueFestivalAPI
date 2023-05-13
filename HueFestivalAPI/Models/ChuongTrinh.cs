using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HueFestivalAPI.Models
{
    [Table("ChuongTrinh")]
    public class ChuongTrinh
    {
        [Key]
        public int IdChuongTrinh { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public int TypeInOff { get; set; }
        public decimal Price { get; set; }
        public int TypeProgram { get; set; }
        public int Arrange { get; set; }
        [MaxLength(200)]
        public string? Md5 { get; set; }
        [JsonIgnore]
        public ICollection<ChuongTrinhImage> ChuongTrinhImages { get; set; }
        [JsonIgnore]
        public ICollection<ChuongTrinhDetails> ChuongTrinhDetails { get; set; }
    }
}
