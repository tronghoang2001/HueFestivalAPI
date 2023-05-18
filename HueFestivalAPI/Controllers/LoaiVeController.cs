using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [HttpGet("loaive")]
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
        [HttpPost("loaive")]
        public async Task<IActionResult> AddLoaiVe(AddLoaiVeDTO loaiveDto)
        {
            var loaive = await _loaiVeService.AddLoaiVeAsync(loaiveDto);

            return Ok(loaive);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateLoaiVe/{id}")]
        public async Task<ActionResult<LoaiVe>> UpdateLoaiVe(AddLoaiVeDTO loaiveDto, int id)
        {
            try
            {
                var loaive = await _loaiVeService.UpdateLoaiVeAsync(loaiveDto, id);
                return Ok(loaive);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteLoaiVe/{id}")]
        public async Task<ActionResult> DeleteLoaiVe(int id)
        {
            await _loaiVeService.DeleteLoaiVeAsync(id);
            return Ok();
        }
    }
}
