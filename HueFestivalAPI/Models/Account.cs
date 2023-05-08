using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int IdAccount { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required, EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [MaxLength(20)]
        public string Role { get; set; }
        public Boolean Status { get; set; }
        public int IdQuyen { get; set; }
        public Quyen Quyen { get; set; }
        public ICollection<TinTuc> TinTucs { get; set; }
        public ICollection<DiaDiem> DiaDiems { get; set; }
    }
}
