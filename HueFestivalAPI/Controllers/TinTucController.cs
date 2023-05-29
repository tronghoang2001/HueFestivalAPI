using HueFestivalAPI.DTO.TinTuc;
using HueFestivalAPI.Services.IServices;
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

        [HttpGet("list-tintuc")]
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

        [HttpGet("tintuc-by-id/{id}")]
        public async Task<IActionResult> GetTinTucById(int id)
        {
            var tinTuc = await _tinTucService.GetTinTucByIdAsync(id);
            return tinTuc == null ? NotFound() : Ok(tinTuc);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-tintuc")]
        public async Task<IActionResult> AddTinTuc([FromForm] AddTinTucDTO tinTucDto, IFormFile imageFile)
        {
            try
            {
                var tinTuc = await _tinTucService.AddTinTucAsync(tinTucDto, imageFile);
                return Ok(tinTuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-tintuc/{id}")]
        public async Task<IActionResult> UpdateTinTuc([FromForm] AddTinTucDTO tinTucDto, int id, IFormFile imageFile)
        {
            try
            {
                var tinTuc = await _tinTucService.UpdateTinTucAsync(tinTucDto, id, imageFile);
                return Ok(tinTuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-tintuc/{id}")]
        public async Task<ActionResult> DeleteTinTuc(int id)
        {
            var result = await _tinTucService.DeleteTinTucAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
