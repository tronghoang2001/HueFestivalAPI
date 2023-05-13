using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddDiemBanVeDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public int SoLuong { get; set; }
        public List<int> VeIds { get; set; }
    }
}
