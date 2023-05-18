using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface ILoaiVeService
    {
        public Task<List<LoaiVeDTO>> GetAllLoaiVeAsync();
        public Task<LoaiVe> AddLoaiVeAsync(AddLoaiVeDTO loaiveDto);
        public Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiveDto, int id);
        public Task DeleteLoaiVeAsync(int id);
    }
}
