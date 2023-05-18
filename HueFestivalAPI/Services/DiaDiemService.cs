using AutoMapper;
using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Net.Mime.MediaTypeNames;

namespace HueFestivalAPI.Services
{
    public class DiaDiemService : IDiaDiemService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public DiaDiemService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DiaDiemMenuDTO>> GetAllMenuAsync()
        {
            var menus = await _context.DiaDiemMenus
                .Include(m => m.DiaDiemSubMenus)
                .ToListAsync();

            return _mapper.Map<List<DiaDiemMenuDTO>>(menus);
        }

        public async Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu)
        {
            var diaDiems = await _context.DiaDiems
                .Where(d => d.IdSubMenu == idSubMenu)
                .ToListAsync();

            var diaDiemDto = _mapper.Map<List<DiaDiemDTO>>(diaDiems);

            return diaDiemDto;
        }

        public async Task<ChiTietDiaDiemDTO> GetDiaDiemByIdAsync(int id)
        {
            var diaDiem = await _context.DiaDiems
                .FirstOrDefaultAsync(c => c.IdDiaDiem == id);

            if (diaDiem == null)
            {
                return null;
            }

            var diaDiemDto = _mapper.Map<ChiTietDiaDiemDTO>(diaDiem);

            return diaDiemDto;
        }

        public async Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto)
        {
            var diaDiemMenu = _mapper.Map<DiaDiemMenu>(diaDiemMenuDto);

            await _context.DiaDiemMenus.AddAsync(diaDiemMenu);
            await _context.SaveChangesAsync();

            return diaDiemMenu;
        }

        public async Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto)
        {
            var diaDiemSubMenu = _mapper.Map<DiaDiemSubMenu>(diaDiemSubMenuDto);
            await _context.DiaDiemSubMenus.AddAsync(diaDiemSubMenu);
            await _context.SaveChangesAsync();
            return diaDiemSubMenu;
        }

        public async Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto)
        {
            var diaDiem = _mapper.Map<DiaDiem>(diaDiemDto);
            await _context.DiaDiems.AddAsync(diaDiem);
            await _context.SaveChangesAsync();
            return diaDiem;
        }

        public async Task<DiaDiemMenu> UpdateDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, int id)
        {
            var diaDiemMenu = await _context.DiaDiemMenus.FindAsync(id);
            if (diaDiemMenu == null)
            {
                return null;
            }
            _mapper.Map(diaDiemMenuDto, diaDiemMenu);
            _context.DiaDiemMenus.Update(diaDiemMenu);
            await _context.SaveChangesAsync();
            return diaDiemMenu;
        }

        public async Task<DiaDiemSubMenu> UpdateDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id)
        {
            var diaDiemSubMenu = await _context.DiaDiemSubMenus.FindAsync(id);
            if (diaDiemSubMenu == null)
            {
                return null;
            }
            _mapper.Map(diaDiemSubMenuDto, diaDiemSubMenu);
            _context.DiaDiemSubMenus.Update(diaDiemSubMenu);
            await _context.SaveChangesAsync();
            return diaDiemSubMenu;
        }

        public async Task<DiaDiem> UpdateDiaDiemAsync(AddDiaDiemDTO diaDiemDto, int id)
        {
            var diaDiem = await _context.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return null;
            }
            _mapper.Map(diaDiemDto, diaDiem);
            _context.DiaDiems.Update(diaDiem);
            await _context.SaveChangesAsync();
            return diaDiem;
        }

        public async Task DeleteDiaDiemMenuAsync(int id)
        {
            var diaDiemMenu = await _context.DiaDiemMenus
                .FirstOrDefaultAsync(c => c.IdMenu == id);

            if (diaDiemMenu != null)
            {
                _context.DiaDiemMenus.Remove(diaDiemMenu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDiaDiemSubMenuAsync(int id)
        {
            var diaDiemSubMenu = await _context.DiaDiemSubMenus
                .FirstOrDefaultAsync(c => c.IdSubMenu == id);

            if (diaDiemSubMenu != null)
            {
                _context.DiaDiemSubMenus.Remove(diaDiemSubMenu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDiaDiemAsync(int id)
        {
            var diaDiem = await _context.DiaDiems
                .FirstOrDefaultAsync(c => c.IdDiaDiem == id);

            if (diaDiem != null)
            {
                _context.DiaDiems.Remove(diaDiem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
