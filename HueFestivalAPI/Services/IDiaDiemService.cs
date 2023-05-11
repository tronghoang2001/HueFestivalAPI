using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services
{
    public interface IDiaDiemService
    {
        public Task<List<DiaDiemMenuDTO>> GetAllMenuAsync();
        public Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu);
        public Task<ChiTietDiaDiemDTO> GetDiaDiemByIdAsync(int id);
        public Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto);
        public Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto);
        public Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto);
        public Task<DiaDiemMenu> UpdateDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, int id);
        public Task<DiaDiemSubMenu> UpdateDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id);
        public Task<DiaDiem> UpdateDiaDiemAsync(AddDiaDiemDTO diaDiemDto, int id);
        public Task DeleteDiaDiemMenuAsync(int id);
        public Task DeleteDiaDiemSubMenuAsync(int id);
        public Task DeleteDiaDiemAsync(int id);
    }
}
