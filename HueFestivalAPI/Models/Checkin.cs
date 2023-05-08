﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalAPI.Models
{
    [Table("Checkin")]
    public class Checkin
    {
        [Key]
        public int IdCheckin { get; set; }
        public DateTime DateCheckin { get; set; }
        public int IdHoaDon { get; set; }
        public HoaDon HoaDon { get; set; }
    }
}
