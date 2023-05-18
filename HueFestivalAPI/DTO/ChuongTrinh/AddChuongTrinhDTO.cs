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
        public List<ChuongTrinhImageDTO> Images { get; set; }
        public List<UpdateChuongTrinhDetailsDTO> Details { get; set; }
    }
}
