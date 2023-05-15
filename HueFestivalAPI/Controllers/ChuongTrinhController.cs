using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuongTrinhController : ControllerBase
    {
        private readonly IChuongTrinhService _chuongTrinhService;
        private readonly HueFestivalContext _context;
        public ChuongTrinhController(IChuongTrinhService chuongTrinhService, HueFestivalContext context)
        {
            _chuongTrinhService = chuongTrinhService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChuongTrinh()
        {
            try
            {
                return Ok(await _chuongTrinhService.GetAllChuongTrinhAsync());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChuongTrinhById(int id)
        {
            var chuongtrinh = await _chuongTrinhService.GetChuongTrinhByIdAsync(id);
            return chuongtrinh == null ? NotFound() : Ok(chuongtrinh);
        }

        [HttpGet("ngay")]
        public async Task<ActionResult<List<LichDienDTO>>> GetNgayWithSoLuongChuongTrinh()
        {
            var ngays = await _chuongTrinhService.GetNgayWithSoLuongChuongTrinhAsync();
            return Ok(ngays);
        }
        [HttpGet("ngay/{ngay}")]
        public async Task<ActionResult<List<ChuongTrinhTheoNgayDTO>>> GetChuongTrinhByNgay(DateTime ngay)
        {
            var chuongTrinhs = await _chuongTrinhService.GetChuongTrinhByNgayAsync(ngay);

            return chuongTrinhs;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("chuongtrinh")]
        public async Task<IActionResult> AddChuongTrinh(AddChuongTrinhDTO chuongTrinhDto)
        {
            try
            {
                var chuongtrinh = await _chuongTrinhService.AddChuongTrinhAsync(chuongTrinhDto);
                return Ok(chuongtrinh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{idchuongtrinh}/images/{idimage}")]
        public async Task<IActionResult> UpdateChuongTrinhImage(ChuongTrinhImageDTO imageDto, int idchuongtrinh, int idimage)
        {
            try
            {
                var image = await _chuongTrinhService.UpdateImageChuongTrinhAsync(imageDto, idchuongtrinh, idimage);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChuongTrinh(int id)
        {
            await _chuongTrinhService.DeleteChuongTrinhAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChuongTrinh(UpdateChuongTrinhDTO chuongTrinhDto, int id)
        {
            try
            {
                var chuongtrinh = await _chuongTrinhService.UpdateChuongTrinhAsync(chuongTrinhDto, id);
                return Ok(chuongtrinh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{idchuongtrinh}/details/{id_details}")]
        public async Task<IActionResult> UpdateChuongTrinhDetails(UpdateChuongTrinhDetailsDTO detailsDto, int idchuongtrinh, int id_details)
        {
            try
            {
                var details = await _chuongTrinhService.UpdateChuongTrinhDetailsAsync(detailsDto, idchuongtrinh, id_details);
                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{idchuongtrinh}/details/{id_details}")]
        public async Task<ActionResult> DeleteChuongTrinhDetails(int idchuongtrinh, int id_details)
        {
            await _chuongTrinhService.DeleteChuongTrinhDetailsAsync(idchuongtrinh, id_details);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{idchuongtrinh}/image/{idimage}")]
        public async Task<ActionResult> DeleteChuongTrinhImage(int idchuongtrinh, int idimage)
        {
            await _chuongTrinhService.DeleteChuongTrinhImageAsync(idchuongtrinh, idimage);
            return Ok();
        }
    }
}
