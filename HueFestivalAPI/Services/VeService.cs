using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
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
            var loaive = new LoaiVe
            {
                Name = loaiveDto.name
            };

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
            var loaives = await _context.LoaiVes
               .ToListAsync();
            return loaives.Select(c => new LoaiVeDTO
            {
                id = c.IdLoaiVe,
                name = c.Name
            }).ToList();
        }

        public async Task<LoaiVe> UpdateLoaiVeAsync(AddLoaiVeDTO loaiveDto, int id)
        {
            var loaive = await _context.LoaiVes.FindAsync(id);

            if (loaive == null)
            {
                return null;
            }

            loaive.Name = loaiveDto.name;

            _context.LoaiVes.Update(loaive);
            await _context.SaveChangesAsync();

            return loaive;
        }
        public async Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int id_details)
        {
            var ve = new Ve
            {
                GiaVe = veDto.GiaVe,
                SoLuong = veDto.SoLuong,
                NgayPhatHanh = DateTime.Now,
                IdDetails = id_details,
                IdLoaiVe = veDto.IdLoaiVe
            };

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
            return ves.Select(c => new VeDTO
            {
                id = c.IdVe,
                gia = c.GiaVe,
                soluong = c.SoLuong,
                ngayphathanh = c.NgayPhatHanh,
                loaive = c.LoaiVe.Name,
                chuongtrinh_name = c.ChuongTrinhDetails.ChuongTrinh.Name,
                diadiem_name = c.ChuongTrinhDetails.DiaDiem.Title
            }).ToList();
        }

        public async Task<List<DiemBanVeDTO>> GetAllDiemBanVeAsync()
        {
            var diembanves = await _context.DiemBanVes
                .Include(c => c.ChiTietDiemBanVes)
                    .ThenInclude(c => c.Ve)
                    .ThenInclude(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.ChuongTrinh)
                .ToListAsync();
            return diembanves.Select(c => new DiemBanVeDTO
            {
                id = c.IdDiemBanVe,
                name = c.Name,
                address = c.Address,
                phonenumber = c.PhoneNumber,
                longtitude = c.Longtitude,
                latitude = c.Latitude,
                details_list = c.ChiTietDiemBanVes.Select(d => new ChiTietDiemBanVeDTO
                {
                    id = d.IdChiTietDiemBanVe,
                    chuongtrinh_name = d.Ve.ChuongTrinhDetails.ChuongTrinh.Name,
                    soluong = d.SoLuong
                }).ToList()
            }).ToList();
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
