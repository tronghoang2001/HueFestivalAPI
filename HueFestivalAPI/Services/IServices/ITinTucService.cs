using HueFestivalAPI.DTO.TinTuc;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface ITinTucService
    {
        public Task<object> GetAllTinTucAsync(int pageIndex, int pageSize);
        public Task<object> GetTinTucByIdAsync(int id);
        public Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto, IFormFile imageFile);
        public Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id, IFormFile imageFile);
        public Task<bool> DeleteTinTucAsync(int id);
    }
}
