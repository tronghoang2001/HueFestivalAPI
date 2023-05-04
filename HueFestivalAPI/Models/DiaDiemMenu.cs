using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("DiaDiemMenu")]
    public class DiaDiemMenu
    {
        [Key]
        public int IdMenu { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string PathIcon { get; set; }
        public int TypeData { get; set; }
        public ICollection<DiaDiemSubMenu> DiaDiemSubMenus { get; set; }
    }
}
