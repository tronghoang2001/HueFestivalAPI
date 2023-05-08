namespace HueFestivalAPI.DTO
{
    public class ChuongTrinhTheoNgayDTO
    {
        public string fdate { get; set; }
        public int type { get; set; }
        public string md5 { get; set; }
        public List<ChuongTrinhDetailsTheoNgayDTO> details_list { get; set; }
    }
}
