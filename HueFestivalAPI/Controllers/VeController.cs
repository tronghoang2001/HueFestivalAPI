using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("list-ve")]
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
        [HttpPost("phat-hanh-ve/{id_details}")]
        public async Task<IActionResult> PhatHanhVe(AddVeDTO veDto, int idDetails)
        {
            var ve = await _veService.PhatHanhVeAsync(veDto, idDetails);

            return Ok(ve);
        }

        [HttpPost("dat-ve")]
        public async Task<object> DatVe(ThongTinDatVeDTO thongTinDatVeDTO)
        {
            var thongTinDatVe = await _veService.AddThongTinDatVe(thongTinDatVeDTO);
            if (thongTinDatVe != null)
            {
                var result = new
                {
                    message = "Đặt vé thành công!",
                    detail = thongTinDatVe
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("Số lượng vé còn lại không đủ!");
            }
        }

        [HttpPost("thanh-toan")]
        public async Task<object> ThanhToan(ThongTinThanhToanDTO thongTinThanhToanDTO, int idThongTin)
        {
            try
            {
                var qrCodes = await _veService.ThanhToanAsync(thongTinThanhToanDTO, idThongTin);
                if (qrCodes != null)
                {
                    var result = new
                    {
                        message = "Thanh toán thành công!",
                        list = qrCodes
                    };
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Thanh toán thất bại!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("checkin")]
        public async Task<IActionResult> Checkin(string qrCode)
        {
            try
            {
                var result = await _veService.CheckinAsync(qrCode);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Vé không hợp lệ hoặc đã Checkin rồi!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
