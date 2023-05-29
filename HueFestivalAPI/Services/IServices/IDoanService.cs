using HueFestivalAPI.DTO.Doan;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IDoanService
    {
        public Task<List<DoanChuongTrinhDTO>> GetAllDoanAsync();
        public Task<DoanChuongTrinh> AddDoanAsync(AddDoanDTO doanDto);
        public Task<DoanChuongTrinh> UpdateDoanAsync(AddDoanDTO doanDto, int id);
        public Task<bool> DeleteDoanAsync(int id);
    }
}
