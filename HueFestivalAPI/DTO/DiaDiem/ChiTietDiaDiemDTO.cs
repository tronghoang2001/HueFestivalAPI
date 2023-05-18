namespace HueFestivalAPI.DTO.DiaDiem
{
    public class ChiTietDiaDiemDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public string pathimage { get; set; }
        public DateTime postdate { get; set; }
        public string lang { get; set; }
        public string author { get; set; }
        public double latitude { get; set; }
        public double longtitude { get; set; }
    }
}
