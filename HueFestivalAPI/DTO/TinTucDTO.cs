using System.Reflection;

namespace HueFestivalAPI.DTO
{
    public class TinTucDTO
    {
        public int id {  get; set; }
        public string title { get; set; }
        public string pathimage { get; set; }
        public string summary { get; set; }
        public DateTime postdate { get; set; }
        public DateTime changedate { get; set; }    
    }
}
