using HueFestivalAPI.DTO;

namespace HueFestivalAPI.Services
{
    public interface ITinTucService
    {
        public Task<List<TinTucDTO>> GetAllTinTucAsync();
        public Task<ChiTietTinTucDTO> GetTinTucByIdAsync(int id);
    }
}
