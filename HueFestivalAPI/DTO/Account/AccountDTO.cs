using System.ComponentModel.DataAnnotations;

namespace HueFestivalAPI.DTO.Account
{
    public class AccountDTO
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string role { get; set; }
        public bool status { get; set; }
    }
}
