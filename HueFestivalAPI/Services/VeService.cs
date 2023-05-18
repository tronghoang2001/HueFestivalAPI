using AutoMapper;
using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class VeService : IVeService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public VeService(HueFestivalContext context, IMapper mapper)
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
        public async Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int id_details)
        {
            var ve = _mapper.Map<Ve>(veDto);
            ve.NgayPhatHanh = DateTime.Now;
            ve.IdDetails = id_details;
            await _context.Ves.AddAsync(ve);
            await _context.SaveChangesAsync();
            return ve;
        }

        public async Task<List<VeDTO>> GetAllVeAsync()
        {
            var ves = await _context.Ves
                .Include(c => c.LoaiVe)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.ChuongTrinh)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .ToListAsync();

            var veDtos = _mapper.Map<List<VeDTO>>(ves);

            return veDtos;
        }
    }
}
