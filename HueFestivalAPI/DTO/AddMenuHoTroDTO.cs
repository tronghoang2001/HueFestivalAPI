using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddMenuHoTroDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
