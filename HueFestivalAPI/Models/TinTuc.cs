using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Principal;

namespace HueFestivalAPI.Models
{
    [Table("TinTuc")]
    public class TinTuc
    {
        [Key]
        public int IdTinTuc { get; set; }
        public int TypeId { get; set; }
        public int? OtherTypeId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string? Url { get; set; }
        [MaxLength(50)]
        public string? Keywords { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Content { get; set; }
        [MaxLength(200)]
        public string? PathFile { get; set; }
        [Required]
        [MaxLength(200)]
        public string PathImage { get; set; }
        [MaxLength(200)]
        public string? Video { get; set; }
        [MaxLength(200)]
        public string? Comment { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ChangeDate { get; set;}
        public int Approved { get; set; }
        public Boolean? IsNew { get; set; }
        public Boolean? IsFocus { get; set; }
        public Boolean? IsHome { get; set; }
        public int View { get; set; }
        public int Arrange { get; set; }
        public int Latitude { get; set; }
        public int Longtitude { get; set; }
        public int IdAccount { get; set; }
        public Account Account { get; set; }
    }
}
