using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("list-diadiem-menu")]
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

        [HttpGet("diadiem-by-id-submenu")]
        public async Task<object> GetDiaDiemByIdSubMenu(int idSubMenu, int pageIndex, int pageSize)
        {
            var diaDiems = await _diaDiemService.GetDiaDiemByIdSubMenuAsync(idSubMenu, pageIndex, pageSize);
            return diaDiems;
        }

        [HttpGet("diadiem-by-id/{id}")]
        public async Task<IActionResult> GetDiaDiemById(int id)
        {
            var diaDiem = await _diaDiemService.GetDiaDiemByIdAsync(id);
            return diaDiem == null ? NotFound() : Ok(diaDiem);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-diadiem-menu")]
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
        [HttpPost("create-diadiem-submenu")]
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
        [HttpPost("create-diadiem")]
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
        [HttpPut("update-diadiem-menu/{id}")]
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
        [HttpPut("update-diadiem-submenu/{id}")]
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
        [HttpPut("update-diadiem/{id}")]
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
        [HttpDelete("delete-diadiem-menu/{id}")]
        public async Task<ActionResult> DeleteDiaDiemMenu(int id)
        {
            var result = await _diaDiemService.DeleteDiaDiemMenuAsync(id);
            if(result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-diadiem-submenu/{id}")]
        public async Task<ActionResult> DeleteDiaDiemSubMenu(int id)
        {
            var result = await _diaDiemService.DeleteDiaDiemSubMenuAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-diadiem/{id}")]
        public async Task<ActionResult> DeleteDiaDiem(int id)
        {
            var result = await _diaDiemService.DeleteDiaDiemAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
