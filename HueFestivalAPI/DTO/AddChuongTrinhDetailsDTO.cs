using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddChuongTrinhDetailsDTO
    {
        public string Time { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int IdDiaDiem { get; set; }
        public int IdNhom { get; set; }
    }
}
