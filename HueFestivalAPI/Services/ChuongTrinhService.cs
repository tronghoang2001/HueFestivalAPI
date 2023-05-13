﻿using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

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
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.NhomChuongTrinh)
                .Include(c => c.ChuongTrinhImages)
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
                    time = d.Time.ToString(),
                    fdate = d.StartDate.ToString("yyyy-MM-dd"),
                    tdate = d.EndDate.ToString("yyyy-MM-dd"),
                    id_doan = 0,
                    doan_name = null,
                    id_diadiem = d.IdDiaDiem,
                    diadiem_name = d.DiaDiem.Title,
                    id_nhom = d.IdNhom,
                    nhom_name = d.NhomChuongTrinh.Name
                }).ToList(),
                pathimage_list = c.ChuongTrinhImages.Select(i => i.PathImage).ToList()
            }).ToList();
        }

        public async Task<ChuongTrinhDTO> GetChuongTrinhByIdAsync(int idChuongTrinh)
        {
            var chuongTrinh = await _context.ChuongTrinhs
            .Include(c => c.ChuongTrinhImages)
            .Include(c => c.ChuongTrinhDetails)
                .ThenInclude(c => c.DiaDiem)
            .Include(c => c.ChuongTrinhDetails)
                .ThenInclude(c => c.NhomChuongTrinh)
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
                    time = d.Time.ToString(),
                    fdate = d.StartDate.ToString("yyyy-MM-dd"),
                    tdate = d.EndDate.ToString("yyyy-MM-dd"),
                    id_doan = 0,
                    doan_name = null,
                    id_diadiem = d.IdDiaDiem,
                    diadiem_name = d.DiaDiem.Title,
                    id_nhom = d.IdNhom,
                    nhom_name = d.NhomChuongTrinh.Name
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
            .Where(x => x.SoLuongChuongTrinh > 0)
            .ToList();

            return result;
        }

        public async Task<List<ChuongTrinhTheoNgayDTO>> GetChuongTrinhByNgayAsync(DateTime ngay)
        {
            var chuongTrinhs = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
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
                        diadiem_name = d.DiaDiem.Title,
                        fdate = d.StartDate.ToString("yyyy-MM-dd")
                    })
                    .ToList()
            }).ToList();

            return chuongTrinhTheoNgayDTOs;
        }

        public async Task<ChuongTrinh> AddChuongTrinhAsync(AddChuongTrinhDTO chuongTrinhDto)
        {
            var program = new ChuongTrinh
            {
                Name = chuongTrinhDto.Name,
                Content = chuongTrinhDto.Content,
                TypeInOff = chuongTrinhDto.TypeInOff,
                Price = chuongTrinhDto.Price,
                TypeProgram = chuongTrinhDto.TypeProgram,
                Arrange = chuongTrinhDto.Arrange,
                ChuongTrinhDetails = new List<ChuongTrinhDetails>(),
                ChuongTrinhImages = new List<ChuongTrinhImage>()
            };

            foreach (var imageModel in chuongTrinhDto.Images)
            {
                var image = new ChuongTrinhImage
                {
                    PathImage = imageModel.pathimage
                };
                program.ChuongTrinhImages.Add(image);
            }

            foreach (var detailModel in chuongTrinhDto.Details)
            {
                var detail = new ChuongTrinhDetails
                {
                    Time = TimeSpan.Parse(detailModel.Time),
                    StartDate = DateTime.Parse(detailModel.StartDate),
                    EndDate = DateTime.Parse(detailModel.EndDate),
                    IdDiaDiem = detailModel.IdDiaDiem,
                    IdNhom = detailModel.IdNhom
                };
                program.ChuongTrinhDetails.Add(detail);
            }

            try
            {
                _context.ChuongTrinhs.Add(program);
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

            return program;
        }

        public async Task<ChuongTrinhImage> UpdateImageChuongTrinhAsync(ChuongTrinhImageDTO imageDto, int idchuongtrinh, int idimage)
        {
            var image = await _context.ChuongTrinhImages.FindAsync(idimage);

            if (image == null)
            {
                return null;
            }

            if(image.IdChuongTrinh != idchuongtrinh)
            {
                return null;
            }

            image.PathImage = imageDto.pathimage;

            _context.ChuongTrinhImages.Update(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task DeleteChuongTrinhAsync(int id)
        {
            var chuongTrinh = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                .Include(c => c.ChuongTrinhImages)
                .FirstOrDefaultAsync(c => c.IdChuongTrinh == id);

            if (chuongTrinh != null)
            {
                _context.ChuongTrinhDetails.RemoveRange(chuongTrinh.ChuongTrinhDetails);
                _context.ChuongTrinhImages.RemoveRange(chuongTrinh.ChuongTrinhImages);
                _context.ChuongTrinhs.Remove(chuongTrinh);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ChuongTrinh> UpdateChuongTrinhAsync(UpdateChuongTrinhDTO chuongTrinhDto, int id)
        {
            var chuongtrinh = await _context.ChuongTrinhs.FindAsync(id);

            if (chuongtrinh == null)
            {
                return null;
            }

            chuongtrinh.Name = chuongTrinhDto.Name;
            chuongtrinh.Content = chuongTrinhDto.Content;
            chuongtrinh.TypeInOff = chuongTrinhDto.TypeInOff;
            chuongtrinh.Price = chuongTrinhDto.Price;
            chuongtrinh.TypeProgram = chuongTrinhDto.TypeProgram;
            chuongtrinh.Arrange = chuongTrinhDto.Arrange;

            _context.ChuongTrinhs.Update(chuongtrinh);
            await _context.SaveChangesAsync();
            return chuongtrinh;
        }

        public async Task<ChuongTrinhDetails> UpdateChuongTrinhDetailsAsync(UpdateChuongTrinhDetailsDTO detailsDto, int idchuongtrinh, int id_details)
        {
            var details = await _context.ChuongTrinhDetails.FindAsync(id_details);

            if (details == null)
            {
                return null;
            }

            if(details.IdChuongTrinh != idchuongtrinh)
            {
                return null;
            }

            details.Time = TimeSpan.Parse(detailsDto.Time);
            details.StartDate = DateTime.Parse(detailsDto.StartDate);
            details.EndDate = DateTime.Parse(detailsDto.EndDate);
            details.IdDiaDiem = detailsDto.IdDiaDiem;
            details.IdNhom = detailsDto.IdNhom;

            _context.ChuongTrinhDetails.Update(details);
            await _context.SaveChangesAsync();
            return details;
        }

        public async Task DeleteChuongTrinhDetailsAsync(int idchuongtrinh, int id_details)
        {
            var details = await _context.ChuongTrinhDetails
                .FirstOrDefaultAsync(c => c.IdChuongTrinh == idchuongtrinh);

            if (details != null && details.IdDetails == id_details)
            {
                _context.ChuongTrinhDetails.Remove(details);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteChuongTrinhImageAsync(int idchuongtrinh, int idimage)
        {
            var image = await _context.ChuongTrinhImages
                .FirstOrDefaultAsync(c => c.IdImage == idimage);

            if (image != null && image.IdChuongTrinh == idchuongtrinh)
            {
                _context.ChuongTrinhImages.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
