using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.Account
{
    public class AddQuyenDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public List<int> ChucNangIds { get; set; }
    }
}
