using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IDiaDiemService
    {
        public Task<List<DiaDiemMenuDTO>> GetAllMenuAsync();
        public Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu, int pageIndex, int pageSize);
        public Task<ChiTietDiaDiemDTO> GetDiaDiemByIdAsync(int id);
        public Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, IFormFile iconFile);
        public Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, IFormFile iconFile);
        public Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto, IFormFile imageFile);
        public Task<DiaDiemMenu> UpdateDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, int id, IFormFile iconFile);
        public Task<DiaDiemSubMenu> UpdateDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id, IFormFile iconFile);
        public Task<DiaDiem> UpdateDiaDiemAsync(AddDiaDiemDTO diaDiemDto, int id, IFormFile imageFile);
        public Task DeleteDiaDiemMenuAsync(int id);
        public Task DeleteDiaDiemSubMenuAsync(int id);
        public Task DeleteDiaDiemAsync(int id);
    }
}
