using AutoMapper;
using HueFestivalAPI.DTO.Account;
using HueFestivalAPI.Helpers;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace HueFestivalAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HueFestivalContext _context;
        private readonly GenerateToken _generateToken;
        private readonly IMapper _mapper;

        public AccountService(HueFestivalContext context, IMapper mapper, GenerateToken generateToken)
        {
            _context = context;
            _mapper = mapper;
            _generateToken = generateToken;
        }

        public async Task<Account> AddAccountAsync(AddAccountDTO accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);

            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }


        public Task<string> LoginAsync(LoginDTO loginDto)
        {
            var account = Authenticate(loginDto);
            if (account != null)
            {
                var token = _generateToken.CalculateToken(account);
                return Task.FromResult(token);
            }
            return Task.FromResult(string.Empty);
        }

        private Account Authenticate(LoginDTO loginDto)
        {
            var currentUser = _context.Account.FirstOrDefault(a => a.Email.ToLower() ==
                loginDto.email.ToLower());
            if (currentUser != null && currentUser.Password == MD5Encryption.CalculateMD5(loginDto.password))
            {
                return currentUser;
            }
            return null;
        }

        public async Task<Account> AdminUpdateAccountAsync(AdminUpdateAccountDTO accountDto, int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return null;
            }

            var quyen = await _context.Quyens.FindAsync(accountDto.IdQuyen);

            account.IdQuyen = accountDto.IdQuyen;
            account.Status = accountDto.Status;
            account.Role = quyen.Name;

            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> UserUpdateAccountAsync(UserUpdateAccountDTO accountDto, int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return null;
            }

            account.FullName = accountDto.FullName;
            account.PhoneNumber = accountDto.PhoneNumber;

            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<List<AccountDTO>> GetAllAccountAsync()
        {
            var accounts = await _context.Account.ToListAsync();
            return _mapper.Map<List<AccountDTO>>(accounts);
        }

        public async Task<Account> ChangePasswordAsync(ChangePasswordDTO accountDto, string email)
        {
            var account = await _context.Account.FirstOrDefaultAsync(a => a.Email == email);

            if (account == null)
            {
                return null;
            }

            if (account.Password != MD5Encryption.CalculateMD5(accountDto.OldPassword))
            {
                throw new Exception("Mật khẩu cũ không hợp lệ!");
            }

            if (accountDto.NewPassword != accountDto.ConfirmPassword)
            {
                throw new Exception("Xác nhận mật khẩu không trùng khớp!");
            }

            account.Password = MD5Encryption.CalculateMD5(accountDto.NewPassword);
            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<List<ChucNangDTO>> GetAllChucNangAsync()
        {
            var chucNangs = await _context.ChucNangs
               .ToListAsync();
            return chucNangs.Select(c => new ChucNangDTO
            {
                id = c.IdChucNang,
                name = c.Name
            }).ToList();
        }

        public async Task<ChucNang> AddChucNangAsync(AddChucNangDTO chucNangDto)
        {
            var chucNang = new ChucNang
            {
                Name = chucNangDto.Name
            };

            await _context.ChucNangs.AddAsync(chucNang);
            await _context.SaveChangesAsync();

            return chucNang;
        }

        public async Task<ChucNang> UpdateChucNangAsync(AddChucNangDTO chucNangDto, int id)
        {
            var chucNang = await _context.ChucNangs.FindAsync(id);

            if (chucNang == null)
            {
                return null;
            }

            chucNang.Name = chucNangDto.Name;

            _context.ChucNangs.Update(chucNang);
            await _context.SaveChangesAsync();

            return chucNang;
        }

        public async Task<bool> DeleteChucNangAsync(int id)
        {
            var chucNang = await _context.ChucNangs
                .FirstOrDefaultAsync(c => c.IdChucNang == id);

            if (chucNang != null)
            {
                _context.ChucNangs.Remove(chucNang);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Quyen> AddQuyenAsync(AddQuyenDTO quyenDto)
        {
            var quyen = new Quyen
            {
                Name = quyenDto.Name,
                PhanQuyenChucNangs = new List<PhanQuyenChucNang>()
            };

            foreach (var chucNangId in quyenDto.ChucNangIds)
            {
                var chucNang = await _context.ChucNangs.FindAsync(chucNangId);
                if (chucNang != null)
                {
                    var phanQuyenChucNang = new PhanQuyenChucNang
                    {
                        Quyen = quyen,
                        ChucNang = chucNang
                    };
                    quyen.PhanQuyenChucNangs.Add(phanQuyenChucNang);
                }
            }

            await _context.Quyens.AddAsync(quyen);
            await _context.SaveChangesAsync();

            return quyen;
        }

        public async Task<List<QuyenDTO>> GetAllQuyenAsync()
        {
            var quyens = await _context.Quyens
                .Include(c => c.PhanQuyenChucNangs)
                    .ThenInclude(c => c.ChucNang)
                .ToListAsync();

            return _mapper.Map<List<QuyenDTO>>(quyens);
        }

        public async Task<bool> DeleteQuyenAsync(int id)
        {
            var quyen = await _context.Quyens
                .Include(c => c.PhanQuyenChucNangs)
                .FirstOrDefaultAsync(c => c.IdQuyen == id);

            if (quyen != null)
            {
                _context.PhanQuyenChucNangs.RemoveRange(quyen.PhanQuyenChucNangs);
                _context.Quyens.Remove(quyen);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _context.Account.FirstOrDefaultAsync(a => a.Email == email);
            if (user == null)
            {
                return false;
            }

            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            await _context.SaveChangesAsync();

            return await SendPasswordResetEmailAsync(user.Email, token);
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDto, string email)
        {
            var user = await _context.Account.FirstOrDefaultAsync(a => a.Email == email);
            if (user == null)
            {
                return false;
            }
            if (user.ResetToken != resetPasswordDto.ResetToken)
            {
                return false;
            }
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                throw new Exception("Xác nhận mật khẩu không trùng khớp!");
            }
            user.Password = MD5Encryption.CalculateMD5(resetPasswordDto.NewPassword);
            user.ResetToken = null;
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> SendPasswordResetEmailAsync(string email, string token)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender Name", "12a9nth@gmail.com"));
                message.To.Add(new MailboxAddress(email, email)); 

                message.Subject = "Password Reset";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<p>Mã xác nhận khôi phục mật khẩu của bạn: {token}</p>";

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("sandbox.smtp.mailtrap.io", 465, false); 
                    await client.AuthenticateAsync("36b82a08f96a82", "b102352bb625e9");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
