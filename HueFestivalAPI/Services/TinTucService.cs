using AutoMapper;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System;
using HueFestivalAPI.Services.Interfaces;
using HueFestivalAPI.DTO.TinTuc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task<List<TinTucDTO>> GetAllTinTucAsync(int pageIndex, int pageSize)
        {
            var tintucs = await _context.TinTucs
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var tintucDtos = _mapper.Map<List<TinTucDTO>>(tintucs);
            return tintucDtos;
        }

        public async Task<ChiTietTinTucDTO> GetTinTucByIdAsync(int id)
        {
            var tintuc = await _context.TinTucs.FirstOrDefaultAsync(t => t.IdTinTuc == id);
            if (tintuc == null)
            {
                return null;
            }
            return _mapper.Map<ChiTietTinTucDTO>(tintuc);
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
            var tintuc = await _context.TinTucs.FindAsync(id);
            if (tintuc == null)
            {
                return null;
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("Uploads\\TinTucImage", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                tintuc.PathImage = fileName;
            }
            _mapper.Map(tinTucDto, tintuc);
            _context.TinTucs.Update(tintuc);
            await _context.SaveChangesAsync();
            return tintuc;
        }


        public async Task DeleteTinTucAsync(int id)
        {
            var tintuc = await _context.TinTucs
                .FirstOrDefaultAsync(c => c.IdTinTuc == id);

            if (tintuc != null)
            {
                _context.TinTucs.Remove(tintuc);
                await _context.SaveChangesAsync();
            }
        }

    }
}
