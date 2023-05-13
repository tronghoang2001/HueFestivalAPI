using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class DiemBanVeDTO
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        [MaxLength(200)]
        public string address { get; set; }
        [MaxLength(15)]
        public string phonenumber { get; set; }
        public double longtitude { get; set; }
        public double latitude { get; set; }
        public List<ChiTietDiemBanVeDTO> details_list { get; set; }
    }
}
