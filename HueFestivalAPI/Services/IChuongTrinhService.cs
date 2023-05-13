using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services
{
    public interface IChuongTrinhService
    {
        public Task<List<ChuongTrinhDTO>> GetAllChuongTrinhAsync();
        public Task<ChuongTrinhDTO> GetChuongTrinhByIdAsync(int id);
        public Task<List<LichDienDTO>> GetNgayWithSoLuongChuongTrinhAsync();
        public Task<List<ChuongTrinhTheoNgayDTO>> GetChuongTrinhByNgayAsync(DateTime ngay);
        public Task<ChuongTrinh> AddChuongTrinhAsync(AddChuongTrinhDTO chuongTrinhDto);
        public Task<ChuongTrinh> UpdateChuongTrinhAsync(UpdateChuongTrinhDTO chuongTrinhDto, int id);
        public Task DeleteChuongTrinhAsync(int id);
        public Task<ChuongTrinhDetails> UpdateChuongTrinhDetailsAsync(UpdateChuongTrinhDetailsDTO detailsDto, int idchuongtrinh, int id_details);
        public Task<ChuongTrinhImage> UpdateImageChuongTrinhAsync(ChuongTrinhImageDTO imageDto, int idchuongtrinh, int idimage);
        public Task DeleteChuongTrinhDetailsAsync(int idchuongtrinh, int id_details);
        public Task DeleteChuongTrinhImageAsync(int idchuongtrinh, int idimage);
    }
}
