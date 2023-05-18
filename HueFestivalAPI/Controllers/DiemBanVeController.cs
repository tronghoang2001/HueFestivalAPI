using HueFestivalAPI.DTO.DiemBanVe;
using HueFestivalAPI.Services;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HueFestivalAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DiemBanVeController : ControllerBase
    {
        private readonly IDiemBanVeService _diemBanVeService;
        public DiemBanVeController(IDiemBanVeService diemBanVeService)
        {
            _diemBanVeService = diemBanVeService;
        }
        [HttpGet("diemBanVe")]
        public async Task<IActionResult> GetAllDiemBanVe()
        {
            try
            {
                return Ok(await _diemBanVeService.GetAllDiemBanVeAsync());
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
            var ve = await _diemBanVeService.AddDiemBanVeAsync(diemBanVeDto);

            return Ok(ve);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDiemBanVe/{id}")]
        public async Task<ActionResult> DeleteDiemBanVe(int id)
        {
            await _diemBanVeService.DeleteDiemBanVeAsync(id);
            return Ok();
        }
    }
}
