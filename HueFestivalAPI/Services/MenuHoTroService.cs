using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class MenuHoTroService : IMenuHoTroService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public MenuHoTroService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuHoTroDTO>> GetAllMenuHoTroAsync()
        {

            var menus = await _context.MenuHoTros
                .ToListAsync();
            return menus.Select(m => new MenuHoTroDTO
            {
                id = m.IdHoTro,
                title = m.Title,
            }).ToList();
        }

        public async Task<ChiTietMenuHoTroDTO> GetMenuHoTroByIdAsync(int id)
        {
            var menu = await _context.MenuHoTros
            .FirstOrDefaultAsync(m => m.IdHoTro == id);

            if (menu == null)
            {
                return null;
            }

            var menuDto = new ChiTietMenuHoTroDTO
            {
                id = menu.IdHoTro,
                title = menu.Title,
                content = menu.Content
            };

            return menuDto;
        }

        public async Task<MenuHoTro> AddMenuAsync(AddMenuHoTroDTO menuHoTroDto)
        {
            var menu = new MenuHoTro
            {
                Title = menuHoTroDto.Title,
                Content = menuHoTroDto.Content,
            };

            await _context.MenuHoTros.AddAsync(menu);
            await _context.SaveChangesAsync();

            return menu;
        }
        public async Task<MenuHoTro> UpdateMenuAsync(AddMenuHoTroDTO menuHoTroDto, int id)
        {
            var menu = await _context.MenuHoTros.FindAsync(id);

            if (menu == null)
            {
                return null;
            }

            menu.Title = menuHoTroDto.Title;
            menu.Content = menuHoTroDto.Content;

            _context.MenuHoTros.Update(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task DeleteMenuAsync(int id)
        {
            var menu = await _context.MenuHoTros
                .FirstOrDefaultAsync(c => c.IdHoTro == id);

            if (menu != null)
            {
                _context.MenuHoTros.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
