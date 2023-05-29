using HueFestivalAPI.DTO.Account;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
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
        [HttpGet("list-account")]
        public async Task<IActionResult> GetAllAccounts()
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
        [HttpPost("create-account")]
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
        [HttpPut("admin-update-account/{id}")]
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
        [HttpPut("user-update-account/{id}")]
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
        [HttpPut("change-password/{email}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO accountDto, string email)
        {
            try
            {
                var account = await _accountService.ChangePasswordAsync(accountDto, email);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("list-chucnang")]
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
        [HttpPost("create-chucnang")]
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
        [HttpPut("update-chucnang/{id}")]
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
        [HttpDelete("delete-chucnang/{id}")]
        public async Task<ActionResult> DeleteChucNang(int id)
        {
            var result = await _accountService.DeleteChucNangAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("list-quyen")]
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
        [HttpPost("create-quyen")]
        public async Task<IActionResult> AddQuyen(AddQuyenDTO quyenDto)
        {
            var quyen = await _accountService.AddQuyenAsync(quyenDto);

            return Ok(quyen);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-quyen/{id}")]
        public async Task<ActionResult> DeleteQuyen(int id)
        {
            var result = await _accountService.DeleteQuyenAsync(id);
            if(result == false)
            {
                return NotFound();  
            } 
            return Ok("Delete Success!");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var isSuccess = await _accountService.ForgotPassword(email);
            if (!isSuccess)
            {
                return StatusCode(500, "Gửi mã khôi phục đến Email không thành công!");
            }

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDto, string email)
        {
            var isSuccess = await _accountService.ResetPassword(resetPasswordDto, email);
            if (!isSuccess)
            {
                return BadRequest("Mã khôi phục không hợp lệ!");
            }

            return Ok();
        }
    }
}
