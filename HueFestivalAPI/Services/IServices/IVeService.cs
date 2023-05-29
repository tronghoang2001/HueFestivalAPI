using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IVeService
    {
        public Task<List<VeDTO>> GetAllVeAsync();
        public Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int idDetails);
        public Task<ThongTinDatVe> AddThongTinDatVe(ThongTinDatVeDTO thongTinDatVeDTO);
        public Task<List<string>> ThanhToanAsync(ThongTinThanhToanDTO thongTinThanhToanDTO, int idThongTin);
        public Task<object> CheckinAsync(string qrCode);
    }
}
