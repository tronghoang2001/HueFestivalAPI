using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomController : ControllerBase
    {
        private readonly INhomService _nhomService;
        public NhomController(INhomService nhomService)
        {
            _nhomService = nhomService;
        }

        [HttpGet("list-nhom")]
        public async Task<IActionResult> GetAllNhom()
        {
            try
            {
                return Ok(await _nhomService.GetAllNhomAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-nhom")]
        public async Task<IActionResult> AddNhom(AddNhomDTO nhomDto)
        {
            var nhom = await _nhomService.AddNhomAsync(nhomDto);

            return Ok(nhom);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-nhom/{id}")]
        public async Task<ActionResult<NhomChuongTrinh>> UpdateNhom(AddNhomDTO nhomDto, int id)
        {
            try
            {
                var nhom = await _nhomService.UpdateNhomAsync(nhomDto, id);
                return Ok(nhom);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-nhom/{id}")]
        public async Task<ActionResult> DeleteNhom(int id)
        {
            var result = await _nhomService.DeleteNhomAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
