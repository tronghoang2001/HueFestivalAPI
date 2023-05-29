using AutoMapper;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;
using HueFestivalAPI.Services.IServices;
using HueFestivalAPI.DTO.TinTuc;

namespace HueFestivalAPI.Services
{
    public class TinTucService : ITinTucService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public TinTucService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetAllTinTucAsync(int pageIndex, int pageSize)
        {
            var tinTucs = await _context.TinTucs
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var tinTucDtos = _mapper.Map<List<TinTucDTO>>(tinTucs);
            var result = new
            {
                type = 1,
                list = tinTucDtos
            };
            return result;
        }

        public async Task<object> GetTinTucByIdAsync(int id)
        {
            var tinTuc = await _context.TinTucs.FirstOrDefaultAsync(t => t.IdTinTuc == id);
            if (tinTuc == null)
            {
                return null;
            }
            var tintucDto = _mapper.Map<ChiTietTinTucDTO>(tinTuc);
            var result = new
            {
                type = 1,
                detail = tintucDto
            };
            return result;
        }

        public async Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto, IFormFile imageFile)
        {
            var tinTuc = _mapper.Map<TinTuc>(tinTucDto);
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("Uploads\\TinTucImage", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                tinTuc.PathImage = fileName;
            }
            await _context.TinTucs.AddAsync(tinTuc);
            await _context.SaveChangesAsync();
            return tinTuc;
        }

        public async Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id, IFormFile imageFile)
        {
            var tinTuc = await _context.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return null;
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var filePath = Path.Combine("Uploads\\TinTucImage", tinTuc.PathImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                filePath = Path.Combine("Uploads\\TinTucImage", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                tinTuc.PathImage = fileName;
            }
            _mapper.Map(tinTucDto, tinTuc);
            _context.TinTucs.Update(tinTuc);
            await _context.SaveChangesAsync();
            return tinTuc;
        }


        public async Task<bool> DeleteTinTucAsync(int id)
        {
            var tinTuc = await _context.TinTucs
                .FirstOrDefaultAsync(c => c.IdTinTuc == id);

            if (tinTuc != null)
            {
                var filePath = Path.Combine("Uploads\\TinTucImage", tinTuc.PathImage);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                _context.TinTucs.Remove(tinTuc);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
