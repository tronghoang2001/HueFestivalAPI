using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IDiaDiemService
    {
        public Task<object> GetAllMenuAsync();
        public Task<object> GetDiaDiemByIdSubMenuAsync(int idSubMenu, int pageIndex, int pageSize);
        public Task<object> GetDiaDiemByIdAsync(int id);
        public Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, IFormFile iconFile);
        public Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, IFormFile iconFile);
        public Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto, IFormFile imageFile);
        public Task<DiaDiemMenu> UpdateDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, int id, IFormFile iconFile);
        public Task<DiaDiemSubMenu> UpdateDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id, IFormFile iconFile);
        public Task<DiaDiem> UpdateDiaDiemAsync(AddDiaDiemDTO diaDiemDto, int id, IFormFile imageFile);
        public Task<bool> DeleteDiaDiemMenuAsync(int id);
        public Task<bool> DeleteDiaDiemSubMenuAsync(int id);
        public Task<bool> DeleteDiaDiemAsync(int id);
    }
}
