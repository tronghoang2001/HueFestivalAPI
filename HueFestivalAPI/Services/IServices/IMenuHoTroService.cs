using HueFestivalAPI.DTO.MenuHoTro;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IMenuHoTroService
    {
        public Task<List<MenuHoTroDTO>> GetAllMenuHoTroAsync();
        public Task<ChiTietMenuHoTroDTO> GetMenuHoTroByIdAsync(int id);
        public Task<MenuHoTro> AddMenuAsync(AddMenuHoTroDTO menuHoTroDto);
        public Task<MenuHoTro> UpdateMenuAsync(AddMenuHoTroDTO menuHoTroDto, int id);
        public Task DeleteMenuAsync(int id);
    }
}
