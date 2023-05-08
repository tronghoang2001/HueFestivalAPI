using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            return menus.Select(m => new DiaDiemMenuDTO
            {
                id = m.IdMenu,
                title = m.Title,
                pathicon = m.PathIcon,
                typedata = m.TypeData,
                submenu = m.DiaDiemSubMenus.Select(s => new DiaDiemSubMenuDTO
                {
                    id = s.IdSubMenu,
                    title = s.Title,
                    pathicon = s.PathIcon,
                    ptypeid = s.IdMenu,
                    typedata = s.TypeData,
                }).ToList()
            }).ToList();
        }

        public async Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu)
        {
            var diaDiems = await _context.DiaDiems
               .Where(d => d.IdSubMenu == idSubMenu)
               .ToListAsync();

            var diaDiemDto = diaDiems.Select(c => new DiaDiemDTO
            {
                id = c.IdDiaDiem,
                title = c.Title,
                summary = c.Summary,
                pathimage = c.PathImage,
                longtitude = c.Longtitude,
                latitude = c.Latitude,
                typedata = c.TypeData
            }).ToList();

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

            var diaDiemDto = new ChiTietDiaDiemDTO
            {
                id = diaDiem.IdDiaDiem,
                title = diaDiem.Title,
                summary = diaDiem.Summary,
                content = diaDiem.Content,
                pathimage = diaDiem.PathImage,
                postdate = diaDiem.PostDate,
                lang = "vn",
                latitude = diaDiem.Latitude,
                longtitude = diaDiem.Longtitude,
            };

            return diaDiemDto;
        }
    }
}
