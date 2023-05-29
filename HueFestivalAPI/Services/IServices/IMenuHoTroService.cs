using HueFestivalAPI.DTO.MenuHoTro;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IMenuHoTroService
    {
        public Task<object> GetAllMenuHoTroAsync();
        public Task<object> GetMenuHoTroByIdAsync(int id);
        public Task<MenuHoTro> AddMenuAsync(AddMenuHoTroDTO menuHoTroDto);
        public Task<MenuHoTro> UpdateMenuAsync(AddMenuHoTroDTO menuHoTroDto, int id);
        public Task<bool> DeleteMenuAsync(int id);
    }
}
