using HueFestivalAPI.DTO;
using HueFestivalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [Authorize(Roles = "Admin")]
        [HttpPost("diaDiemMenu")]
        public async Task<IActionResult> AddDiaDiemMenu(AddDiaDiemMenuDTO diaDiemMenuDto)
        {
            try
            {
                var diaDiemMenu = await _diaDiemService.AddDiaDiemMenuAsync(diaDiemMenuDto);
                return Ok(diaDiemMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("diaDiemSubMenu")]
        public async Task<IActionResult> AddDiaDiemSubMenu(AddDiaDiemSubMenuDTO diaDiemSubMenuDto)
        {
            try
            {
                var diaDiemSubMenu = await _diaDiemService.AddDiaDiemSubMenuAsync(diaDiemSubMenuDto);
                return Ok(diaDiemSubMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("diaDiem")]
        public async Task<IActionResult> AddDiaDiem(AddDiaDiemDTO diaDiemDto)
        {
            try
            {
                var diaDiem = await _diaDiemService.AddDiaDiemAsync(diaDiemDto);
                return Ok(diaDiem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiemMenu/{id}")]
        public async Task<IActionResult> UpdateDiaDiemMenu(AddDiaDiemMenuDTO diaDiemMenuDto, int id)
        {
            try
            {
                var diaDiemMenu = await _diaDiemService.UpdateDiaDiemMenuAsync(diaDiemMenuDto, id);
                return Ok(diaDiemMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiemSubMenu/{id}")]
        public async Task<IActionResult> UpdateDiaDiemSubMenu(AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id)
        {
            try
            {
                var diaDiemSubmenu = await _diaDiemService.UpdateDiaDiemSubMenuAsync(diaDiemSubMenuDto, id);
                return Ok(diaDiemSubmenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiem/{id}")]
        public async Task<IActionResult> UpdateDiaDiem(AddDiaDiemDTO diaDiemDto, int id)
        {
            try
            {
                var diaDiem = await _diaDiemService.UpdateDiaDiemAsync(diaDiemDto, id);
                return Ok(diaDiem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDiaDiemMenu/{id}")]
        public async Task<ActionResult> DeleteDiaDiemMenu(int id)
        {
            await _diaDiemService.DeleteDiaDiemMenuAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDiaDiemSubMenu/{id}")]
        public async Task<ActionResult> DeleteDiaDiemSubMenu(int id)
        {
            await _diaDiemService.DeleteDiaDiemSubMenuAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDiaDiem/{id}")]
        public async Task<ActionResult> DeleteDiaDiem(int id)
        {
            await _diaDiemService.DeleteDiaDiemAsync(id);
            return Ok();
        }
    }
}
