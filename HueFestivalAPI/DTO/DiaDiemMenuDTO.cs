namespace HueFestivalAPI.DTO
{
    public class DiaDiemMenuDTO
    {
        public int id {  get; set; }
        public string title { get; set; }   
        public string pathicon { get; set; }
        public int typedata { get; set; }
        public List<DiaDiemSubMenuDTO> submenu { get; set; }
    }
}
