using HueFestivalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class ChuongTrinhImageDTO
    {
        [MaxLength(200)]
        public string pathimage { get; set; }
    }
}
