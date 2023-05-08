using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class ChuongTrinhService : IChuongTrinhService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public ChuongTrinhService(HueFestivalContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ChuongTrinhDTO>> GetAllChuongTrinhAsync()
        {
            var chuongtrinhs = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhImages)
                .Include(c => c.ChuongTrinhDetails)
                .ToListAsync();
            return chuongtrinhs.Select(c => new ChuongTrinhDTO
            {
                id_chuongtrinh = c.IdChuongTrinh,
                chuongtrinh_name = c.Name,
                chuongtrinh_content = c.Content,
                type_inoff = c.TypeInOff,
                price = c.Price,
                type_program = c.TypeProgram,
                arrange = c.Arrange,
                details_list = c.ChuongTrinhDetails.Select(d => new ChuongTrinhDetailsDTO
                {
                    time = d.Time,
                    fdate = d.StartDate.ToString("yyyy-MM-dd"),
                    tdate = d.EndDate.ToString("yyyy-MM-dd"),
                    id_doan = 0,
                    doan_name = null,
                    id_diadiem = d.IdDiaDiem,
                    diadiem_name = d.DiaDiemName,
                    id_nhom = d.IdNhom,
                    nhom_name = d.NhomName
                }).ToList(),
                pathimage_list = c.ChuongTrinhImages.Select(i => i.PathImage).ToList()
            }).ToList();
        }

        public async Task<ChuongTrinhDTO> GetChuongTrinhByIdAsync(int idChuongTrinh)
        {
            var chuongTrinh = await _context.ChuongTrinhs
            .Include(c => c.ChuongTrinhImages)
            .Include(c => c.ChuongTrinhDetails)
            .FirstOrDefaultAsync(c => c.IdChuongTrinh == idChuongTrinh);

            if (chuongTrinh == null)
            {
                return null;
            }

            var chuongTrinhDto = new ChuongTrinhDTO
            {
                id_chuongtrinh = chuongTrinh.IdChuongTrinh,
                chuongtrinh_name = chuongTrinh.Name,
                chuongtrinh_content = chuongTrinh.Content,
                type_inoff = chuongTrinh.TypeInOff,
                price = chuongTrinh.Price,
                type_program = chuongTrinh.TypeProgram,
                arrange = chuongTrinh.Arrange,
                details_list = chuongTrinh.ChuongTrinhDetails.Select(d => new ChuongTrinhDetailsDTO
                {
                    time = d.Time,
                    fdate = d.StartDate.ToString("yyyy-MM-dd"),
                    tdate = d.EndDate.ToString("yyyy-MM-dd"),
                    id_doan = 0,
                    doan_name = null,
                    id_diadiem = d.IdDiaDiem,
                    diadiem_name = d.DiaDiemName,
                    id_nhom = d.IdNhom,
                    nhom_name = d.NhomName
                }).ToList(),
                pathimage_list = chuongTrinh.ChuongTrinhImages.Select(i => i.PathImage).ToList()
            };

            return chuongTrinhDto;
        }

        public async Task<List<LichDienDTO>> GetNgayWithSoLuongChuongTrinhAsync()
        {
            var firstDay = await _context.ChuongTrinhDetails
                .OrderBy(c => c.StartDate)
                .FirstOrDefaultAsync();

            var lastDay = await _context.ChuongTrinhDetails
                .OrderByDescending(c => c.EndDate)
                .FirstOrDefaultAsync();

            var ngays = Enumerable.Range(0, 1 + (int)(lastDay.EndDate.Date - firstDay.StartDate.Date).TotalDays)
                .Select(i => firstDay.StartDate.AddDays(i).Date)
                .ToList();

            var result = ngays.Select(g => new LichDienDTO
            {
                Ngay = g.ToString("yyyy-MM-dd"),
                SoLuongChuongTrinh = _context.ChuongTrinhDetails.Count(d => d.StartDate.Date <= g && g <= d.EndDate.Date)
            })
            .ToList();

            return result;
        }

        public async Task<List<ChuongTrinhTheoNgayDTO>> GetChuongTrinhByNgayAsync(DateTime ngay)
        {
            var chuongTrinhs = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                .Where(c => c.ChuongTrinhDetails.Any(d => d.StartDate.Date <= ngay.Date && d.EndDate.Date >= ngay.Date))
                .ToListAsync();

            var chuongTrinhTheoNgayDTOs = chuongTrinhs.Select(c => new ChuongTrinhTheoNgayDTO
            {
                fdate = ngay.ToString("yyyy-MM-dd"),
                type = 0,
                details_list = c.ChuongTrinhDetails
                    .Where(d => d.StartDate.Date <= ngay.Date && d.EndDate.Date >= ngay.Date)
                    .Select(d => new ChuongTrinhDetailsTheoNgayDTO
                    {
                        time = d.Time,
                        id_chuongtrinh = c.IdChuongTrinh,
                        chuongtrinh_name = c.Name,
                        type_inoff = c.TypeInOff,
                        tdate = d.EndDate.ToString("yyyy-MM-dd"),
                        id_diadiem = d.IdDiaDiem,
                        diadiem_name = d.DiaDiemName,
                        fdate = d.StartDate.ToString("yyyy-MM-dd")
                    })
                    .ToList()
            }).ToList();

            return chuongTrinhTheoNgayDTOs;
        }
    }
}
