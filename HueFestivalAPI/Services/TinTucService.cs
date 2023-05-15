using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System;
using HueFestivalAPI.Services.Interfaces;

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

        public async Task<List<TinTucDTO>> GetAllTinTucAsync()
        {
            var tintucs = await _context.TinTucs.ToListAsync();
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

        public async Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto)
        {
            var tinTuc = _mapper.Map<TinTuc>(tinTucDto);
            await _context.TinTucs.AddAsync(tinTuc);
            await _context.SaveChangesAsync();
            return tinTuc;
        }

        public async Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id)
        {
            var tintuc = await _context.TinTucs.FindAsync(id);

            if (tintuc == null)
            {
                return null;
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
