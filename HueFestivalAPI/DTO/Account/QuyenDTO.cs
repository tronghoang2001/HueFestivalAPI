using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HueFestivalAPI.DTO.Account
{
    public class QuyenDTO
    {
        public int id { get; set; }
        public string quyen_name { get; set; }
        public List<PhanQuyenChucNangDTO> chucnang_list { get; set; }
    }
}
