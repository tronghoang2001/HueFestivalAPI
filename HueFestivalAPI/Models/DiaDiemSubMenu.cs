using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace HueFestivalAPI.Models
{
    [Table("DiaDiemSubMenu")]
    public class DiaDiemSubMenu
    {
        [Key]
        public int IdSubMenu { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string PathIcon { get; set; }
        public int PTypeId { get; set; }
        public int TypeData { get; set; }
        public int IdMenu { get; set; }
        public DiaDiemMenu DiaDiemMenu { get; set; }
        public ICollection<DiaDiem> DiaDiems { get; set; }
    }
}
