using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Services.Interfaces;
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

        [HttpGet("getDiaDiemBySubMenu")]
        public async Task<ActionResult<List<DiaDiemDTO>>> GetDiaDiemByIdSubMenu(int idSubMenu, int pageIndex, int pageSize)
        {
            var diaDiems = await _diaDiemService.GetDiaDiemByIdSubMenuAsync(idSubMenu, pageIndex, pageSize);
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
        public async Task<IActionResult> AddDiaDiemMenu([FromForm] AddDiaDiemMenuDTO diaDiemMenuDto, IFormFile iconFile)
        {
            try
            {
                var diaDiemMenu = await _diaDiemService.AddDiaDiemMenuAsync(diaDiemMenuDto, iconFile);
                return Ok(diaDiemMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("diaDiemSubMenu")]
        public async Task<IActionResult> AddDiaDiemSubMenu([FromForm] AddDiaDiemSubMenuDTO diaDiemSubMenuDto, IFormFile iconFile)
        {
            try
            {
                var diaDiemSubMenu = await _diaDiemService.AddDiaDiemSubMenuAsync(diaDiemSubMenuDto, iconFile);
                return Ok(diaDiemSubMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("diaDiem")]
        public async Task<IActionResult> AddDiaDiem([FromForm] AddDiaDiemDTO diaDiemDto, IFormFile imageFile)
        {
            try
            {
                var diaDiem = await _diaDiemService.AddDiaDiemAsync(diaDiemDto, imageFile);
                return Ok(diaDiem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiemMenu/{id}")]
        public async Task<IActionResult> UpdateDiaDiemMenu([FromForm] AddDiaDiemMenuDTO diaDiemMenuDto, int id, IFormFile iconFile)
        {
            try
            {
                var diaDiemMenu = await _diaDiemService.UpdateDiaDiemMenuAsync(diaDiemMenuDto, id, iconFile);
                return Ok(diaDiemMenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiemSubMenu/{id}")]
        public async Task<IActionResult> UpdateDiaDiemSubMenu([FromForm] AddDiaDiemSubMenuDTO diaDiemSubMenuDto, int id, IFormFile iconFile)
        {
            try
            {
                var diaDiemSubmenu = await _diaDiemService.UpdateDiaDiemSubMenuAsync(diaDiemSubMenuDto, id, iconFile);
                return Ok(diaDiemSubmenu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateDiaDiem/{id}")]
        public async Task<IActionResult> UpdateDiaDiem([FromForm] AddDiaDiemDTO diaDiemDto, int id, IFormFile imageFile)
        {
            try
            {
                var diaDiem = await _diaDiemService.UpdateDiaDiemAsync(diaDiemDto, id, imageFile);
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
