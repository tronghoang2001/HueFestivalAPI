using HueFestivalAPI.DTO;

namespace HueFestivalAPI.Services
{
    public interface IDiaDiemService
    {
        public Task<List<DiaDiemMenuDTO>> GetAllMenuAsync();
        public Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu);
        public Task<ChiTietDiaDiemDTO> GetDiaDiemByIdAsync(int id);
    }
}
