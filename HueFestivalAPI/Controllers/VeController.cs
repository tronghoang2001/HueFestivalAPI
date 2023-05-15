using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeController : ControllerBase
    {
        private readonly IVeService _veService;
        public VeController(IVeService veService)
        {
            _veService = veService;
        }

        [HttpGet("loaive")]
        public async Task<IActionResult> GetAllLoaiVe()
        {
            try
            {
                return Ok(await _veService.GetAllLoaiVeAsync());
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
            var loaive = await _veService.AddLoaiVeAsync(loaiveDto);

            return Ok(loaive);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateLoaiVe/{id}")]
        public async Task<ActionResult<LoaiVe>> UpdateLoaiVe(AddLoaiVeDTO loaiveDto, int id)
        {
            try
            {
                var loaive = await _veService.UpdateLoaiVeAsync(loaiveDto, id);
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
            await _veService.DeleteLoaiVeAsync(id);
            return Ok();
        }

        [HttpGet("ve")]
        public async Task<IActionResult> GetAllVe()
        {
            try
            {
                return Ok(await _veService.GetAllVeAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("phatHanhVe/{id_details}")]
        public async Task<IActionResult> PhatHanhVe(AddVeDTO veDto, int id_details)
        {
            var ve = await _veService.PhatHanhVeAsync(veDto, id_details);

            return Ok(ve);
        }

        [HttpGet("diemBanVe")]
        public async Task<IActionResult> GetAllDiemBanVe()
        {
            try
            {
                return Ok(await _veService.GetAllDiemBanVeAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("diemBanVe")]
        public async Task<IActionResult> AddDiemBanVe(AddDiemBanVeDTO diemBanVeDto)
        {
            var ve = await _veService.AddDiemBanVeAsync(diemBanVeDto);

            return Ok(ve);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDiemBanVe/{id}")]
        public async Task<ActionResult> DeleteDiemBanVe(int id)
        {
            await _veService.DeleteDiemBanVeAsync(id);
            return Ok();
        }

    }
}
