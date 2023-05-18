using AutoMapper;
using HueFestivalAPI.DTO.Doan;
using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class DoanService : IDoanService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;
        public DoanService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DoanChuongTrinh> AddDoanAsync(AddDoanDTO doanDto)
        {
            var doan = _mapper.Map<DoanChuongTrinh>(doanDto);
            await _context.DoanChuongTrinhs.AddAsync(doan);
            await _context.SaveChangesAsync();
            return doan;
        }

        public async Task DeleteDoanAsync(int id)
        {
            var doan = await _context.DoanChuongTrinhs
                .FirstOrDefaultAsync(c => c.IdDoan == id);

            if (doan != null)
            {
                _context.DoanChuongTrinhs.Remove(doan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<DoanChuongTrinhDTO>> GetAllDoanAsync()
        {
            var doans = await _context.DoanChuongTrinhs.ToListAsync();
            return _mapper.Map<List<DoanChuongTrinhDTO>>(doans);
        }

        public async Task<DoanChuongTrinh> UpdateDoanAsync(AddDoanDTO doanDto, int id)
        {
            var doan = await _context.DoanChuongTrinhs.FindAsync(id);
            if (doan == null)
            {
                return null;
            }
            _mapper.Map(doanDto, doan);
            _context.DoanChuongTrinhs.Update(doan);
            await _context.SaveChangesAsync();
            return doan;
        }
    }
}
