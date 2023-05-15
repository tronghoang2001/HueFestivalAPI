using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("account")]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                return Ok(await _accountService.GetAllAccountAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(AddAccountDTO accountDto)
        {
            try
            {
                var account = await _accountService.AddAccountAsync(accountDto);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("adminUpdateAccount/{id}")]
        public async Task<IActionResult> AdminUpdateAccount(AdminUpdateAccountDTO accountDto, int id)
        {
            try
            {
                var account = await _accountService.AdminUpdateAccountAsync(accountDto, id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("userUpdateAccount/{id}")]
        public async Task<ActionResult<Account>> UserUpdateAccount(UserUpdateAccountDTO accountDto, int id)
        {
            try
            {
                var account = await _accountService.UserUpdateAccountAsync(accountDto, id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO accountDto, int id)
        {
            try
            {
                var account = await _accountService.ChangePasswordAsync(accountDto, id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("chucnang")]
        public async Task<IActionResult> GetAllChucNang()
        {
            try
            {
                return Ok(await _accountService.GetAllChucNangAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("chucnang")]
        public async Task<IActionResult> AddChucNang(AddChucNangDTO chucNangDto)
        {
            try
            {
                var chucnang = await _accountService.AddChucNangAsync(chucNangDto);
                return Ok(chucnang);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateChucNang/{id}")]
        public async Task<ActionResult<Account>> UpdateChucNang(AddChucNangDTO chucNangDto, int id)
        {
            try
            {
                var chucnang = await _accountService.UpdateChucNangAsync(chucNangDto, id);
                return Ok(chucnang);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteChucNang/{id}")]
        public async Task<ActionResult> DeleteChucNang(int id)
        {
            await _accountService.DeleteChucNangAsync(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("quyen")]
        public async Task<IActionResult> GetAllQuyen()
        {
            try
            {
                return Ok(await _accountService.GetAllQuyenAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("quyen")]
        public async Task<IActionResult> AddQuyen(AddQuyenDTO quyenDto)
        {
            var quyen = await _accountService.AddQuyenAsync(quyenDto);

            return Ok(quyen);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteQuyen/{id}")]
        public async Task<ActionResult> DeleteQuyen(int id)
        {
            await _accountService.DeleteQuyenAsync(id);
            return Ok();
        }
    }
}
