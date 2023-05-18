using HueFestivalAPI.DTO.DiemBanVe;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IDiemBanVeService
    {
        public Task<List<DiemBanVeDTO>> GetAllDiemBanVeAsync();
        public Task<DiemBanVe> AddDiemBanVeAsync(AddDiemBanVeDTO diemBanVeDto);
        public Task DeleteDiemBanVeAsync(int id);
    }
}
