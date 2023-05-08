using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class ChuongTrinhDTO
    {
        public int id_chuongtrinh { get; set; }
        public string chuongtrinh_name { get; set; }
        public string chuongtrinh_content { get; set; }
        public int type_inoff { get; set; }
        public decimal price { get; set; }
        public int type_program { get; set; }
        public int arrange { get; set; }
        public List<ChuongTrinhDetailsDTO> details_list { get; set; }
        [MaxLength(200)]
        public string? md5 { get; set; }
        public List<string> pathimage_list { get; set; }
    }
}
