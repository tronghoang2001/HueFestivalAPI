using HueFestivalAPI.DTO;
using HueFestivalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuongTrinhController : ControllerBase
    {
        private readonly IChuongTrinhService _chuongTrinhService;
        public ChuongTrinhController(IChuongTrinhService chuongTrinhService)
        {
            _chuongTrinhService = chuongTrinhService;
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
    }
}
