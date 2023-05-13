using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddQuyenDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public List<int> ChucNangIds { get; set; }
    }
}
