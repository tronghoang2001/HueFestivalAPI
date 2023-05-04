using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("MenuHoTro")]
    public class MenuHoTro
    {
        [Key]
        public int IdHoTro { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
