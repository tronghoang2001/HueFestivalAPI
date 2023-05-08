using HueFestivalAPI.DTO;

namespace HueFestivalAPI.Services
{
    public interface IChuongTrinhService
    {
        public Task<List<ChuongTrinhDTO>> GetAllChuongTrinhAsync();
        public Task<ChuongTrinhDTO> GetChuongTrinhByIdAsync(int id);
        public Task<List<LichDienDTO>> GetNgayWithSoLuongChuongTrinhAsync();
        public Task<List<ChuongTrinhTheoNgayDTO>> GetChuongTrinhByNgayAsync(DateTime ngay);
    }
}
