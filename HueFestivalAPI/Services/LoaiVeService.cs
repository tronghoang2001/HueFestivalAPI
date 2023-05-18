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

        public async Task<LoaiVe> AddLoaiVeAsync(AddLoaiVeDTO loaiveDto)
        {
            var loaive = _mapper.Map<LoaiVe>(loaiveDto);
            await _context.LoaiVes.AddAsync(loaive);
            await _context.SaveChangesAsync();
            return loaive;
        }

        public async Task DeleteLoaiVeAsync(int id)
        {
            var loaive = await _context.LoaiVes
                .FirstOrDefaultAsync(c => c.IdLoaiVe == id);

            if (loaive != null)
            {
                _context.LoaiVes.Remove(loaive);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LoaiVeDTO>> GetAllLoaiVeAsync()
        {
            var loaives = await _context.LoaiVes.ToListAsync();
            return _mapper.Map<List<LoaiVeDTO>>(loaives);
        }

        public async Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiveDto, int id)
        {
            var loaive = await _context.LoaiVes.FindAsync(id);
            if (loaive == null)
            {
                return null;
            }
            _mapper.Map(loaiveDto, loaive);
            _context.LoaiVes.Update(loaive);
            await _context.SaveChangesAsync();
            return loaive;
        }
    }
}
