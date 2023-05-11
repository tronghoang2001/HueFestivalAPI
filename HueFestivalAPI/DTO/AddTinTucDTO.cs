using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO
{
    public class AddTinTucDTO
    {
        public int TypeId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(200)]
        public string PathImage { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public int Approved { get; set; }
        public int Arrange { get; set; }
        public int Latitude { get; set; }
        public int Longtitude { get; set; }
    }
}
