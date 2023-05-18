using AutoMapper;
using HueFestivalAPI.DTO.DiemBanVe;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class DiemBanVeService : IDiemBanVeService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public DiemBanVeService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DiemBanVeDTO>> GetAllDiemBanVeAsync()
        {
            var diembanves = await _context.DiemBanVes
                .Include(c => c.ChiTietDiemBanVes)
                    .ThenInclude(c => c.Ve)
                    .ThenInclude(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.ChuongTrinh)
                .ToListAsync();

            return _mapper.Map<List<DiemBanVeDTO>>(diembanves);
        }

        public async Task<DiemBanVe> AddDiemBanVeAsync(AddDiemBanVeDTO diemBanVeDto)
        {
            var diembanve = new DiemBanVe
            {
                Name = diemBanVeDto.Name,
                Address = diemBanVeDto.Address,
                PhoneNumber = diemBanVeDto.PhoneNumber,
                Longtitude = diemBanVeDto.Longtitude,
                Latitude = diemBanVeDto.Latitude,
                ChiTietDiemBanVes = new List<ChiTietDiemBanVe>()
            };

            foreach (var VeId in diemBanVeDto.VeIds)
            {
                var ve = await _context.Ves.FindAsync(VeId);
                if (ve != null)
                {
                    var details = new ChiTietDiemBanVe
                    {
                        DiemBanVe = diembanve,
                        Ve = ve,
                        SoLuong = diemBanVeDto.SoLuong
                    };
                    diembanve.ChiTietDiemBanVes.Add(details);
                }
            }

            try
            {
                _context.DiemBanVes.Add(diembanve);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }

                throw new Exception(innerException.Message);
            }

            return diembanve;
        }

        public async Task DeleteDiemBanVeAsync(int id)
        {
            var diemBanVe = await _context.DiemBanVes
                .Include(c => c.ChiTietDiemBanVes)
                .FirstOrDefaultAsync(c => c.IdDiemBanVe == id);

            if (diemBanVe != null)
            {
                _context.ChiTietDiemBanVes.RemoveRange(diemBanVe.ChiTietDiemBanVes);
                _context.DiemBanVes.Remove(diemBanVe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
