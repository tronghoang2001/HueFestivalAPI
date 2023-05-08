using HueFestivalAPI.DTO;

namespace HueFestivalAPI.Services
{
    public interface IMenuHoTroService
    {
        public Task<List<MenuHoTroDTO>> GetAllMenuHoTroAsync();
        public Task<ChiTietMenuHoTroDTO> GetMenuHoTroByIdAsync(int id);
    }
}
