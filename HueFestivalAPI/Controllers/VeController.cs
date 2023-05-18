using HueFestivalAPI.DTO.Ve;
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
    }
}
