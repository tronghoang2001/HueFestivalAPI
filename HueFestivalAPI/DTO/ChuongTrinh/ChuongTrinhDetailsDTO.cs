using HueFestivalAPI.Models;

namespace HueFestivalAPI.DTO.ChuongTrinh
{
    public class ChuongTrinhDetailsDTO
    {
        public string time { get; set; }
        public string fdate { get; set; }
        public string tdate { get; set; }
        public int? id_doan { get; set; }
        public string? doan_name { get; set; }
        public int id_diadiem { get; set; }
        public string? diadiem_name { get; set; }
        public int id_nhom { get; set; }
        public string? nhom_name { get; set; }
    }
}
