using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.ChuongTrinh
{
    public class AddChuongTrinhDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int TypeInOff { get; set; }
        public decimal Price { get; set; }
        public int TypeProgram { get; set; }
        public int Arrange { get; set; }
        public string Time { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int IdDiaDiem { get; set; }
        public int IdNhom { get; set; }
    }
}
