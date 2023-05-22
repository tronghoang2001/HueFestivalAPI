using HueFestivalAPI.DTO.TinTuc;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinTucController : ControllerBase
    {
        private readonly ITinTucService _tinTucService;
        public TinTucController(ITinTucService tinTucService)
        {
            _tinTucService = tinTucService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTinTuc(int pageIndex, int pageSize)
        {
            try
            {
                return Ok(await _tinTucService.GetAllTinTucAsync(pageIndex, pageSize));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTinTucById(int id)
        {
            var tintuc = await _tinTucService.GetTinTucByIdAsync(id);
            return tintuc == null ? NotFound() : Ok(tintuc);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddTinTuc([FromForm] AddTinTucDTO tinTucDto, IFormFile imageFile)
        {
            try
            {
                var tintuc = await _tinTucService.AddTinTucAsync(tinTucDto, imageFile);
                return Ok(tintuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTinTuc([FromForm] AddTinTucDTO tinTucDto, int id, IFormFile imageFile)
        {
            try
            {
                var tintuc = await _tinTucService.UpdateTinTucAsync(tinTucDto, id, imageFile);
                return Ok(tintuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTinTuc(int id)
        {
            await _tinTucService.DeleteTinTucAsync(id);
            return Ok();
        }
    }
}
