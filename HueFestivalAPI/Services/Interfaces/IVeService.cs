using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IVeService
    {
        public Task<List<LoaiVeDTO>> GetAllLoaiVeAsync();
        public Task<LoaiVe> AddLoaiVeAsync(AddLoaiVeDTO loaiveDto);
        public Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiveDto, int id);
        public Task DeleteLoaiVeAsync(int id);
        public Task<List<VeDTO>> GetAllVeAsync();
        public Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int id_details);
        public Task<List<DiemBanVeDTO>> GetAllDiemBanVeAsync();
        public Task<DiemBanVe> AddDiemBanVeAsync(AddDiemBanVeDTO diemBanVeDto);
        public Task DeleteDiemBanVeAsync(int id);
    }
}
