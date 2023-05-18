using HueFestivalAPI.DTO.Doan;
using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services;
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
        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> AddDoan(AddDoanDTO doanDto)
        {
            var doan = await _doanService.AddDoanAsync(doanDto);

            return Ok(doan);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoan(int id)
        {
            await _doanService.DeleteDoanAsync(id);
            return Ok();
        }
    }
}
