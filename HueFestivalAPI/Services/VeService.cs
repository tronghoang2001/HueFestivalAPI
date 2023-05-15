using AutoMapper;
using HueFestivalAPI.DTO;
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
