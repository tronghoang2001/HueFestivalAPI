using HueFestivalAPI.DTO.ChuongTrinh;
using HueFestivalAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("list-chuongtrinh")]
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

        [HttpGet("chuongtrinh-by-id/{id}")]
        public async Task<IActionResult> GetChuongTrinhById(int id)
        {
            var chuongTrinh = await _chuongTrinhService.GetChuongTrinhByIdAsync(id);
            return chuongTrinh == null ? NotFound() : Ok(chuongTrinh);
        }

        [HttpGet("lichdien")]
        public async Task<IActionResult> GetNgayWithSoLuongChuongTrinh()
        {
            var ngays = await _chuongTrinhService.GetNgayWithSoLuongChuongTrinhAsync();
            return Ok(ngays);
        }

        [HttpGet("chuongtrinh-theo-ngay/{ngay}")]
        public async Task<object> GetChuongTrinhByNgay(DateTime ngay)
        {
            var chuongTrinhs = await _chuongTrinhService.GetChuongTrinhByNgayAsync(ngay);

            return chuongTrinhs;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-chuongtrinh")]
        public async Task<IActionResult> AddChuongTrinh([FromForm] AddChuongTrinhDTO chuongTrinhDto, List<IFormFile> imageFiles)
        {
            try
            {
                var chuongTrinh = await _chuongTrinhService.AddChuongTrinhAsync(chuongTrinhDto, imageFiles);
                return Ok(chuongTrinh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{idchuongtrinh}/images/{idimage}")]
        public async Task<IActionResult> UpdateChuongTrinhImage(int idChuongTrinh, int idImage, IFormFile imageFile)
        {
            try
            {
                var image = await _chuongTrinhService.UpdateImageChuongTrinhAsync(idChuongTrinh, idImage, imageFile);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-chuongtrinh/{id}")]
        public async Task<ActionResult> DeleteChuongTrinh(int id)
        {
            var result = await _chuongTrinhService.DeleteChuongTrinhAsync(id);
            if(result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-chuongtrinh/{id}")]
        public async Task<IActionResult> UpdateChuongTrinh(UpdateChuongTrinhDTO chuongTrinhDto, int id)
        {
            try
            {
                var chuongTrinh = await _chuongTrinhService.UpdateChuongTrinhAsync(chuongTrinhDto, id);
                return Ok(chuongTrinh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{idchuongtrinh}/details/{id_details}")]
        public async Task<IActionResult> UpdateChuongTrinhDetails(UpdateChuongTrinhDetailsDTO detailsDto, int idChuongTrinh, int idDetails)
        {
            try
            {
                var details = await _chuongTrinhService.UpdateChuongTrinhDetailsAsync(detailsDto, idChuongTrinh, idDetails);
                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{idchuongtrinh}/details/{id_details}")]
        public async Task<ActionResult> DeleteChuongTrinhDetails(int idChuongtrinh, int idDetails)
        {
            var result = await _chuongTrinhService.DeleteChuongTrinhDetailsAsync(idChuongtrinh, idDetails);
            if(result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{idchuongtrinh}/image/{idimage}")]
        public async Task<ActionResult> DeleteChuongTrinhImage(int idChuongTrinh, int idImage)
        {
            var result = await _chuongTrinhService.DeleteChuongTrinhImageAsync(idChuongTrinh, idImage);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }
    }
}
