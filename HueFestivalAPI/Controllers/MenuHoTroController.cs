using HueFestivalAPI.DTO.MenuHoTro;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("list-menu-hotro")]
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

        [HttpGet("menu-hotro-by-id/{id}")]
        public async Task<IActionResult> GetMenuHoTroById(int id)
        {
            var menu = await _menuHoTroService.GetMenuHoTroByIdAsync(id);
            return menu == null ? NotFound() : Ok(menu);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-menu-hotro")]
        public async Task<IActionResult> AddMenuHoTro(AddMenuHoTroDTO menuHoTroDto)
        {
            try
            {
                var menu = await _menuHoTroService.AddMenuAsync(menuHoTroDto);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-menu-hotro/{id}")]
        public async Task<IActionResult> UpdateMenuHoTro(AddMenuHoTroDTO menuHoTroDto, int id)
        {
            try
            {
                var menu = await _menuHoTroService.UpdateMenuAsync(menuHoTroDto, id);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-menu-hotro/{id}")]
        public async Task<ActionResult> DeleteMenuHoTro(int id)
        {
            var result = await _menuHoTroService.DeleteMenuAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
