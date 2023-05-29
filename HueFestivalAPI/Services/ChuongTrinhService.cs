using AutoMapper;
using HueFestivalAPI.DTO.ChuongTrinh;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
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

        public async Task<object> GetAllChuongTrinhAsync()
        {
            var chuongtrinhs = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.NhomChuongTrinh)
                .Include(c => c.ChuongTrinhImages).ToListAsync();

            var chuongtrinhDTOs = _mapper.Map<List<ChuongTrinhDTO>>(chuongtrinhs);

            var result = new
            {
                type = 1,
                list = chuongtrinhDTOs
            };

            return result;
        }

        public async Task<object> GetChuongTrinhByIdAsync(int idChuongTrinh)
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

            var chuongTrinhDto = _mapper.Map<ChuongTrinhDTO>(chuongTrinh);

            var result = new
            {
                type = 1,
                detail = chuongTrinhDto
            };
            return result;
        }

        public async Task<object> GetNgayWithSoLuongChuongTrinhAsync()
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

            var lichDien = ngays.Select(g => new LichDienDTO
            {
                Ngay = g.ToString("yyyy-MM-dd"),
                SoLuongChuongTrinh = _context.ChuongTrinhDetails.Count(d => d.StartDate.Date <= g && g <= d.EndDate.Date)
            })
            .Where(x => x.SoLuongChuongTrinh > 0)
            .ToList();

            var result = new
            {
                type = 1,
                list = lichDien
            };
            return result;
        }

        public async Task<object> GetChuongTrinhByNgayAsync(DateTime ngay)
        {
            var chuongTrinhs = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .Where(c => c.ChuongTrinhDetails.Any(d => d.StartDate.Date <= ngay.Date && d.EndDate.Date >= ngay.Date))
                .ToListAsync();

            var chuongTrinhTheoNgayDtos = chuongTrinhs.Select(c => new ChuongTrinhTheoNgayDTO
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

            var result = new
            {
                type = 1,
                list = chuongTrinhTheoNgayDtos
            };
            return result;
        }

        public async Task<ChuongTrinh> AddChuongTrinhAsync(AddChuongTrinhDTO chuongTrinhDto, List<IFormFile> imageFiles)
        {
            var chuongTrinh = _mapper.Map<ChuongTrinh>(chuongTrinhDto);

            try
            {
                _context.ChuongTrinhs.Add(chuongTrinh);
                await _context.SaveChangesAsync();

                var chuongTrinhImages = new List<ChuongTrinhImage>();

                foreach (var imageFile in imageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine("Uploads\\ChuongTrinhImage", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        var chuongTrinhImage = new ChuongTrinhImage
                        {
                            PathImage = fileName,
                            IdChuongTrinh = chuongTrinh.IdChuongTrinh 
                        };

                        chuongTrinhImages.Add(chuongTrinhImage);
                    }
                }

                chuongTrinh.ChuongTrinhImages = chuongTrinhImages;

                await _context.SaveChangesAsync();

                var chuongTrinhDetails = new ChuongTrinhDetails
                {
                    Time = TimeSpan.Parse(chuongTrinhDto.Time),
                    StartDate = DateTime.Parse(chuongTrinhDto.StartDate),
                    EndDate = DateTime.Parse(chuongTrinhDto.EndDate),
                    IdDiaDiem = chuongTrinhDto.IdDiaDiem,
                    IdNhom = chuongTrinhDto.IdNhom,
                    IdChuongTrinh = chuongTrinh.IdChuongTrinh 
                };
                _context.ChuongTrinhDetails.Add(chuongTrinhDetails); 
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

            return chuongTrinh;
        }

        public async Task<ChuongTrinhImage> UpdateImageChuongTrinhAsync(int idChuongTrinh, int idImage, IFormFile imageFile)
        {
            var image = await _context.ChuongTrinhImages.FindAsync(idImage);

            if (image == null || image.IdChuongTrinh != idChuongTrinh)
            {
                return null;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var filePath = Path.Combine("Uploads\\ChuongTrinhImage", image.PathImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                filePath = Path.Combine("Uploads\\ChuongTrinhImage", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                image.PathImage = fileName;

                _context.ChuongTrinhImages.Update(image);
                await _context.SaveChangesAsync();
            }

            return image;
        }


        public async Task<bool> DeleteChuongTrinhAsync(int id)
        {
            var chuongTrinh = await _context.ChuongTrinhs
                .Include(c => c.ChuongTrinhDetails)
                .Include(c => c.ChuongTrinhImages)
                .FirstOrDefaultAsync(c => c.IdChuongTrinh == id);

            if (chuongTrinh != null)
            {
                var chuongTrinhImages = await _context.ChuongTrinhImages
                    .Where(ci => ci.IdChuongTrinh == chuongTrinh.IdChuongTrinh)
                    .ToListAsync();

                foreach (var chuongTrinhImage in chuongTrinhImages)
                {
                    var filePath = Path.Combine("Uploads\\ChuongTrinhImage", chuongTrinhImage.PathImage);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                _context.ChuongTrinhDetails.RemoveRange(chuongTrinh.ChuongTrinhDetails);
                _context.ChuongTrinhImages.RemoveRange(chuongTrinh.ChuongTrinhImages);
                _context.ChuongTrinhs.Remove(chuongTrinh);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<ChuongTrinh> UpdateChuongTrinhAsync(UpdateChuongTrinhDTO chuongTrinhDto, int id)
        {
            var chuongTrinh = await _context.ChuongTrinhs.FindAsync(id);

            if (chuongTrinh == null)
            {
                return null;
            }
            _mapper.Map(chuongTrinhDto, chuongTrinh);
            _context.ChuongTrinhs.Update(chuongTrinh);
            await _context.SaveChangesAsync();
            return chuongTrinh;
        }

        public async Task<ChuongTrinhDetails> UpdateChuongTrinhDetailsAsync(UpdateChuongTrinhDetailsDTO detailsDto, int idChuongTrinh, int idDetails)
        {
            var details = await _context.ChuongTrinhDetails.FindAsync(idDetails);

            if (details == null)
            {
                return null;
            }

            if (details.IdChuongTrinh != idChuongTrinh)
            {
                return null;
            }

            details = _mapper.Map(detailsDto, details);

            _context.ChuongTrinhDetails.Update(details);
            await _context.SaveChangesAsync();
            return details;
        }

        public async Task<bool> DeleteChuongTrinhDetailsAsync(int idChuongTrinh, int idDetails)
        {
            var details = await _context.ChuongTrinhDetails
                .FirstOrDefaultAsync(c => c.IdDetails == idDetails);

            if (details != null && details.IdChuongTrinh == idChuongTrinh)
            {
                _context.ChuongTrinhDetails.Remove(details);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteChuongTrinhImageAsync(int idChuongTrinh, int idImage)
        {
            var image = await _context.ChuongTrinhImages
                .FirstOrDefaultAsync(c => c.IdImage == idImage);

            if (image != null && image.IdChuongTrinh == idChuongTrinh)
            {
                _context.ChuongTrinhImages.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
