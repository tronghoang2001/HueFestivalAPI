using HueFestivalAPI.DTO;
using HueFestivalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaDiemController : ControllerBase
    {
        private readonly IDiaDiemService _diaDiemService;
        public DiaDiemController(IDiaDiemService diaDiemService)
        {
            _diaDiemService = diaDiemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                return Ok(await _diaDiemService.GetAllMenuAsync());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("submenu/{idSubMenu}")]
        public async Task<ActionResult<List<DiaDiemDTO>>> GetDiaDiemByIdSubMenu(int idSubMenu)
        {
            var diaDiems = await _diaDiemService.GetDiaDiemByIdSubMenuAsync(idSubMenu);

            return diaDiems;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiaDiemById(int id)
        {
            var diadiem = await _diaDiemService.GetDiaDiemByIdAsync(id);
            return diadiem == null ? NotFound() : Ok(diadiem);
        }
    }
}
