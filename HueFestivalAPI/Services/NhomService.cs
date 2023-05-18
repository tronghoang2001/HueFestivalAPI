using AutoMapper;
using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class NhomService : INhomService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;
        public NhomService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NhomChuongTrinh> AddNhomAsync(AddNhomDTO nhomDto)
        {
            var nhom = _mapper.Map<NhomChuongTrinh>(nhomDto);
            await _context.NhomChuongTrinhs.AddAsync(nhom);
            await _context.SaveChangesAsync();
            return nhom;
        }

        public async Task DeleteNhomAsync(int id)
        {
            var nhom = await _context.NhomChuongTrinhs
                .FirstOrDefaultAsync(c => c.IdNhom == id);

            if (nhom != null)
            {
                _context.NhomChuongTrinhs.Remove(nhom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NhomChuongTrinhDTO>> GetAllNhomAsync()
        {
            var nhoms = await _context.NhomChuongTrinhs.ToListAsync();
            return _mapper.Map<List<NhomChuongTrinhDTO>>(nhoms);
        }

        public async Task<NhomChuongTrinh> UpdateNhomAsync(AddNhomDTO nhomDto, int id)
        {
            var nhom = await _context.NhomChuongTrinhs.FindAsync(id);
            if (nhom == null)
            {
                return null;
            }
            _mapper.Map(nhomDto, nhom);
            _context.NhomChuongTrinhs.Update(nhom);
            await _context.SaveChangesAsync();
            return nhom;
        }
    }
}
