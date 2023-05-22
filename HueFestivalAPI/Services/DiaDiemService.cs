using AutoMapper;
using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<DiaDiemDTO>> GetDiaDiemByIdSubMenuAsync(int idSubMenu, int pageIndex, int pageSize)
        {
            var diaDiems = await _context.DiaDiems
                .Where(d => d.IdSubMenu == idSubMenu)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
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

        public async Task<DiaDiemMenu> AddDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, IFormFile iconFile)
        {
            var diaDiemMenu = _mapper.Map<DiaDiemMenu>(diaDiemMenuDto);
            if (iconFile != null && iconFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine("Uploads\\MenuIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                diaDiemMenu.PathIcon = fileName;
            }
            await _context.DiaDiemMenus.AddAsync(diaDiemMenu);
            await _context.SaveChangesAsync();

            return diaDiemMenu;
        }

        public async Task<DiaDiemSubMenu> AddDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, IFormFile iconFile)
        {
            var diaDiemSubMenu = _mapper.Map<DiaDiemSubMenu>(diaDiemSubMenuDto);
            if (iconFile != null && iconFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine("Uploads\\SubMenuIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                diaDiemSubMenu.PathIcon = fileName;
            }
            await _context.DiaDiemSubMenus.AddAsync(diaDiemSubMenu);
            await _context.SaveChangesAsync();
            return diaDiemSubMenu;
        }

        public async Task<DiaDiem> AddDiaDiemAsync(AddDiaDiemDTO diaDiemDto, IFormFile imageFile)
        {
            var diaDiem = _mapper.Map<DiaDiem>(diaDiemDto);
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("Uploads\\DiaDiemIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                diaDiem.PathImage = fileName;
            }
            await _context.DiaDiems.AddAsync(diaDiem);
            await _context.SaveChangesAsync();
            return diaDiem;
        }

        public async Task<DiaDiemMenu> UpdateDiaDiemMenuAsync(AddDiaDiemMenuDTO diaDiemMenuDto, int id, IFormFile iconFile)
        {
            var diaDiemMenu = await _context.DiaDiemMenus.FindAsync(id);
            if (diaDiemMenu == null)
            {
                return null;
            }
            if (iconFile != null && iconFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine("Uploads\\MenuIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                diaDiemMenu.PathIcon = fileName;
            }
            _mapper.Map(diaDiemMenuDto, diaDiemMenu);
            _context.DiaDiemMenus.Update(diaDiemMenu);
            await _context.SaveChangesAsync();
            return diaDiemMenu;
        }

        public async Task<DiaDiemSubMenu> UpdateDiaDiemSubMenuAsync(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id, IFormFile iconFile)
        {
            var diaDiemSubMenu = await _context.DiaDiemSubMenus.FindAsync(id);
            if (diaDiemSubMenu == null)
            {
                return null;
            }
            if (iconFile != null && iconFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine("Uploads\\SubMenuIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                diaDiemSubMenu.PathIcon = fileName;
            }
            _mapper.Map(diaDiemSubMenuDto, diaDiemSubMenu);
            _context.DiaDiemSubMenus.Update(diaDiemSubMenu);
            await _context.SaveChangesAsync();
            return diaDiemSubMenu;
        }

        public async Task<DiaDiem> UpdateDiaDiemAsync(AddDiaDiemDTO diaDiemDto, int id, IFormFile imageFile)
        {
            var diaDiem = await _context.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return null;
            }
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("Uploads\\DiaDiemIcon", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                diaDiem.PathImage = fileName;
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
