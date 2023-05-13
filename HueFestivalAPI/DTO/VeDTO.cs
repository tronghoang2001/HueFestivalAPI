using HueFestivalAPI.Models;

namespace HueFestivalAPI.DTO
{
    public class VeDTO
    {
        public int id { get; set; }
        public decimal gia { get; set; }
        public int soluong { get; set; }
        public DateTime ngayphathanh { get; set; }
        public string loaive { get; set; }
        public string chuongtrinh_name { get; set; }
        public string diadiem_name { get; set; }
    }
}
