using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class ChuongTrinhImageDTO
    {
        public int id_pathimage { get; set; }
        [MaxLength(200)]
        public string pathimage { get; set; }
        public int id_chuongtrinh { get; set; }
        public ChuongTrinhDTO ChuongTrinh { get; set; }
    }
}
