using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiVeController : ControllerBase
    {
        private readonly ILoaiVeService _loaiVeService;
        public LoaiVeController(ILoaiVeService loaiVeService)
        {
            _loaiVeService = loaiVeService;
        }

        [HttpGet("list-loaive")]
        public async Task<IActionResult> GetAllLoaiVe()
        {
            try
            {
                return Ok(await _loaiVeService.GetAllLoaiVeAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-loaive")]
        public async Task<IActionResult> AddLoaiVe(AddLoaiVeDTO loaiVeDto)
        {
            var loaiVe = await _loaiVeService.AddLoaiVeAsync(loaiVeDto);

            return Ok(loaiVe);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-loaive/{id}")]
        public async Task<ActionResult<LoaiVe>> UpdateLoaiVe(AddLoaiVeDTO loaiVeDto, int id)
        {
            try
            {
                var loaiVe = await _loaiVeService.UpdateLoaiVeAsync(loaiVeDto, id);
                return Ok(loaiVe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-loaive/{id}")]
        public async Task<ActionResult> DeleteLoaiVe(int id)
        {
            var result = await _loaiVeService.DeleteLoaiVeAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
