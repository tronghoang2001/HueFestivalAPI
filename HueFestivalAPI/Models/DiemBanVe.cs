using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HueFestivalAPI.Models
{
    [Table("DiemBanVe")]
    public class DiemBanVe
    {
        [Key]
        public int IdDiemBanVe { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [MaxLength (15)]
        public string PhoneNumber { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        [JsonIgnore]
        public ICollection<ChiTietDiemBanVe> ChiTietDiemBanVes { get; set; }
    }
}
