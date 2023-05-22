using HueFestivalAPI.DTO.TinTuc;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface ITinTucService
    {
        public Task<List<TinTucDTO>> GetAllTinTucAsync(int pageIndex, int pageSize);
        public Task<ChiTietTinTucDTO> GetTinTucByIdAsync(int id);
        public Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto, IFormFile imageFile);
        public Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id, IFormFile imageFile);
        public Task DeleteTinTucAsync(int id);
    }
}
