using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.Interfaces
{
    public interface IVeService
    {
        public Task<List<VeDTO>> GetAllVeAsync();
        public Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int id_details);
    }
}
