using HueFestivalAPI.DTO.ChuongTrinh;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IChuongTrinhService
    {
        public Task<object> GetAllChuongTrinhAsync();
        public Task<ChuongTrinhDTO> GetChuongTrinhByIdAsync(int id);
        public Task<List<LichDienDTO>> GetNgayWithSoLuongChuongTrinhAsync();
        public Task<List<ChuongTrinhTheoNgayDTO>> GetChuongTrinhByNgayAsync(DateTime ngay);
        public Task<ChuongTrinh> AddChuongTrinhAsync(AddChuongTrinhDTO chuongTrinhDto, List<IFormFile> imageFiles);
        public Task<ChuongTrinh> UpdateChuongTrinhAsync(UpdateChuongTrinhDTO chuongTrinhDto, int id);
        public Task DeleteChuongTrinhAsync(int id);
        public Task<ChuongTrinhDetails> UpdateChuongTrinhDetailsAsync(UpdateChuongTrinhDetailsDTO detailsDto, int idchuongtrinh, int id_details);
        public Task<ChuongTrinhImage> UpdateImageChuongTrinhAsync(int idchuongtrinh, int idimage, IFormFile imageFile);
        public Task DeleteChuongTrinhDetailsAsync(int idchuongtrinh, int id_details);
        public Task DeleteChuongTrinhImageAsync(int idchuongtrinh, int idimage);
    }
}
