namespace HueFestivalAPI.DTO
{
    public class ChuongTrinhDetailsTheoNgayDTO
    {
        public TimeSpan time { get; set; }
        public int id_chuongtrinh { get; set; }
        public string chuongtrinh_name { get; set; }
        public int type_inoff { get; set; }
        public string tdate { get; set; }
        public int id_diadiem { get; set; }
        public string diadiem_name { get; set; }
        public string fdate { get; set; }
    }
}
