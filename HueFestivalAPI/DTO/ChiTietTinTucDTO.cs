namespace HueFestivalAPI.DTO
{
    public class ChiTietTinTucDTO
    {
        public int id { get; set; }
        public int typeid { get; set; }
        public int? other_typeid { get; set; }
        public string title { get; set; }
        public string? url { get; set; }
        public string? keywords { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public string? pathfile { get; set; }
        public string pathimage { get; set; }
        public string? video { get; set; }
        public string? comment { get; set; }
        public DateTime postdate { get; set; }
        public DateTime changedate { get; set; }
        public int approved { get; set; }
        public string lang { get; set; }
        public string author { get; set; }
        public Boolean? isnew { get; set; }
        public Boolean? isfocus { get; set; }
        public Boolean? ishome { get; set; }
        public int view { get; set; }
        public int arrange { get; set; }
        public int latitude { get; set; }
        public int longtitude { get; set; }
    }
}
