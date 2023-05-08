using HueFestivalAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllTinTuc()
        {
            try
            {
                return Ok(await _tinTucService.GetAllTinTucAsync());
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
    }
}
