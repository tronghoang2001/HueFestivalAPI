using AutoMapper;
using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class LoaiVeService : ILoaiVeService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public LoaiVeService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoaiVe> AddLoaiVeAsync(AddLoaiVeDTO loaiVeDto)
        {
            var loaiVe = _mapper.Map<LoaiVe>(loaiVeDto);
            await _context.LoaiVes.AddAsync(loaiVe);
            await _context.SaveChangesAsync();
            return loaiVe;
        }

        public async Task<bool> DeleteLoaiVeAsync(int id)
        {
            var loaiVe = await _context.LoaiVes
                .FirstOrDefaultAsync(c => c.IdLoaiVe == id);

            if (loaiVe != null)
            {
                _context.LoaiVes.Remove(loaiVe);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<LoaiVeDTO>> GetAllLoaiVeAsync()
        {
            var loaiVes = await _context.LoaiVes.ToListAsync();
            return _mapper.Map<List<LoaiVeDTO>>(loaiVes);
        }

        public async Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiVeDto, int id)
        {
            var loaiVe = await _context.LoaiVes.FindAsync(id);
            if (loaiVe == null)
            {
                return null;
            }
            _mapper.Map(loaiVeDto, loaiVe);
            _context.LoaiVes.Update(loaiVe);
            await _context.SaveChangesAsync();
            return loaiVe;
        }
    }
}
