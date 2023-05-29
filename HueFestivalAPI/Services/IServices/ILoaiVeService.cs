using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface ILoaiVeService
    {
        public Task<List<LoaiVeDTO>> GetAllLoaiVeAsync();
        public Task<LoaiVe> AddLoaiVeAsync(AddLoaiVeDTO loaiVeDto);
        public Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiVeDto, int id);
        public Task<bool> DeleteLoaiVeAsync(int id);
    }
}
