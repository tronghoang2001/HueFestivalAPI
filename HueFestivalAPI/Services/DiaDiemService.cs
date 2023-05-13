using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
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

        public async Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto)
        {
            var diaDiemMenu = new DiaDiemMenu
            {
               Title = diaDiemMenuDto.Title,
               PathIcon = diaDiemMenuDto.PathIcon,
               TypeData = diaDiemMenuDto.TypeData
            };

            await _context.DiaDiemMenus.AddAsync(diaDiemMenu);
            await _context.SaveChangesAsync();

            return diaDiemMenu;
        }

        public async Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto)
        {
            var diaDiemSubMenu = new DiaDiemSubMenu
            {
                Title = diaDiemSubMenuDto.Title,
                PathIcon = diaDiemSubMenuDto.PathIcon,
                TypeData = diaDiemSubMenuDto.TypeData,
                IdMenu = diaDiemSubMenuDto.IdMenu
            };

            await _context.DiaDiemSubMenus.AddAsync(diaDiemSubMenu);
            await _context.SaveChangesAsync();

            return diaDiemSubMenu;
        }

        public async Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto)
        {
            var diaDiem = new DiaDiem
            {
                Title = diaDiemDto.Title,
                Summary = diaDiemDto.Summary,
                Content = diaDiemDto.Content,
                PathImage = diaDiemDto.PathImage,
                Longtitude = diaDiemDto.Longtitude,
                Latitude = diaDiemDto.Latitude,
                TypeData = diaDiemDto.TypeData,
                PostDate = DateTime.Now,
                IdAccount = diaDiemDto.IdAccount,
                IdSubMenu = diaDiemDto.IdSubMenu,
            };

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

            diaDiemMenu.Title = diaDiemMenuDto.Title;
            diaDiemMenu.PathIcon = diaDiemMenuDto.PathIcon;
            diaDiemMenu.TypeData = diaDiemMenuDto.TypeData;

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

            diaDiemSubMenu.Title = diaDiemSubMenuDto.Title;
            diaDiemSubMenu.PathIcon = diaDiemSubMenuDto.PathIcon;
            diaDiemSubMenu.TypeData = diaDiemSubMenuDto.TypeData;
            diaDiemSubMenu.IdMenu = diaDiemSubMenuDto.IdMenu;

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

            diaDiem.Title = diaDiemDto.Title;
            diaDiem.Summary = diaDiemDto.Summary;
            diaDiem.Content = diaDiemDto.Content;
            diaDiem.PathImage = diaDiemDto.PathImage;
            diaDiem.Longtitude = diaDiemDto.Longtitude;
            diaDiem.Latitude = diaDiemDto.Latitude;
            diaDiem.TypeData = diaDiemDto.TypeData;
            diaDiem.PostDate = DateTime.Now;
            diaDiem.IdAccount = diaDiemDto.IdAccount;
            diaDiem.IdSubMenu = diaDiemDto.IdSubMenu;

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
