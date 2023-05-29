using HueFestivalAPI.DTO.ChuongTrinh;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IChuongTrinhService
    {
        public Task<object> GetAllChuongTrinhAsync();
        public Task<object> GetChuongTrinhByIdAsync(int id);
        public Task<object> GetNgayWithSoLuongChuongTrinhAsync();
        public Task<object> GetChuongTrinhByNgayAsync(DateTime ngay);
        public Task<ChuongTrinh> AddChuongTrinhAsync(AddChuongTrinhDTO chuongTrinhDto, List<IFormFile> imageFiles);
        public Task<ChuongTrinh> UpdateChuongTrinhAsync(UpdateChuongTrinhDTO chuongTrinhDto, int id);
        public Task<bool> DeleteChuongTrinhAsync(int id);
        public Task<ChuongTrinhDetails> UpdateChuongTrinhDetailsAsync(UpdateChuongTrinhDetailsDTO detailsDto, int idChuongTrinh, int idDetails);
        public Task<ChuongTrinhImage> UpdateImageChuongTrinhAsync(int idChuongTrinh, int idImage, IFormFile imageFile);
        public Task<bool> DeleteChuongTrinhDetailsAsync(int idChuongTrinh, int idDetails);
        public Task<bool> DeleteChuongTrinhImageAsync(int idChuongTrinh, int idImage);
    }
}
