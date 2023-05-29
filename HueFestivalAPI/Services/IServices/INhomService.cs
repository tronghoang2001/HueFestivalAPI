using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface INhomService
    {
        public Task<List<NhomChuongTrinhDTO>> GetAllNhomAsync();
        public Task<NhomChuongTrinh> AddNhomAsync(AddNhomDTO nhomDto);
        public Task<NhomChuongTrinh> UpdateNhomAsync(AddNhomDTO nhomDto, int id);
        public Task<bool> DeleteNhomAsync(int id);
    }
}
