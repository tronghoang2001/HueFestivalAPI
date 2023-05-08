using HueFestivalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MenuHoTroController : ControllerBase
    {
        private readonly IMenuHoTroService _menuHoTroService;
        public MenuHoTroController(IMenuHoTroService menuHoTroService)
        {
            _menuHoTroService = menuHoTroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuHoTro()
        {
            try
            {
                return Ok(await _menuHoTroService.GetAllMenuHoTroAsync());
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuHoTroById(int id)
        {
            var menu = await _menuHoTroService.GetMenuHoTroByIdAsync(id);
            return menu == null ? NotFound() : Ok(menu);
        }
    }
}
