using HueFestivalAPI.DTO.Doan;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoanController : ControllerBase
    {
        private readonly IDoanService _doanService;
        public DoanController(IDoanService doanService)
        {
            _doanService = doanService;
        }
        [HttpGet("list-doan")]
        public async Task<IActionResult> GetAllDoan()
        {
            try
            {
                return Ok(await _doanService.GetAllDoanAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-doan")]
        public async Task<IActionResult> AddDoan(AddDoanDTO doanDto)
        {
            var doan = await _doanService.AddDoanAsync(doanDto);

            return Ok(doan);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-doan/{id}")]
        public async Task<ActionResult<NhomChuongTrinh>> UpdateDoan(AddDoanDTO doanDto, int id)
        {
            try
            {
                var doan = await _doanService.UpdateDoanAsync(doanDto, id);
                return Ok(doan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-doan/{id}")]
        public async Task<ActionResult> DeleteDoan(int id)
        {
            var result = await _doanService.DeleteDoanAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
