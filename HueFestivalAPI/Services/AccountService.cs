using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HueFestivalAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HueFestivalContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(HueFestivalContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Account> AddAccountAsync(AddAccountDTO accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);

            await _context.Account.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }


        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            var account = Authenticate(loginDto);
            if (account != null)
            {
                var token = GenerateToken(account);
                return token;
            }
            return string.Empty;
        }

        // To generate token
        private string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        private Account Authenticate(LoginDTO loginDto)
        {
            var currentUser = _context.Account.FirstOrDefault(a => a.Email.ToLower() ==
                loginDto.email.ToLower());
            if (currentUser != null && BCrypt.Net.BCrypt.Verify(loginDto.password, currentUser.Password))
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

        public async Task<Account> ChangePasswordAsync(ChangePasswordDTO accountDto, int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(accountDto.OldPassword, account.Password))
            {
                throw new Exception("Mật khẩu cũ không hợp lệ!");
            }

            if (accountDto.NewPassword != accountDto.ConfirmPassword)
            {
                throw new Exception("Xác nhận mật khẩu không trùng khớp!");
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(accountDto.NewPassword);
            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<List<ChucNangDTO>> GetAllChucNangAsync()
        {
            var chucnangs = await _context.ChucNangs
               .ToListAsync();
            return chucnangs.Select(c => new ChucNangDTO
            {
                id = c.IdChucNang,
                name = c.Name
            }).ToList();
        }

        public async Task<ChucNang> AddChucNangAsync(AddChucNangDTO chucNangDto)
        {
            var chucnang = new ChucNang
            {
                Name = chucNangDto.Name
            };

            await _context.ChucNangs.AddAsync(chucnang);
            await _context.SaveChangesAsync();

            return chucnang;
        }

        public async Task<ChucNang> UpdateChucNangAsync(AddChucNangDTO chucNangDto, int id)
        {
            var chucnang = await _context.ChucNangs.FindAsync(id);

            if (chucnang == null)
            {
                return null;
            }

            chucnang.Name = chucNangDto.Name;

            _context.ChucNangs.Update(chucnang);
            await _context.SaveChangesAsync();

            return chucnang;
        }

        public async Task DeleteChucNangAsync(int id)
        {
            var chucnang = await _context.ChucNangs
                .FirstOrDefaultAsync(c => c.IdChucNang == id);

            if (chucnang != null)
            {
                _context.ChucNangs.Remove(chucnang);
                await _context.SaveChangesAsync();
            }
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

        public async Task DeleteQuyenAsync(int id)
        {
            var quyen = await _context.Quyens
                .Include(c => c.PhanQuyenChucNangs)
                .FirstOrDefaultAsync(c => c.IdQuyen == id);

            if (quyen != null)
            {
                _context.PhanQuyenChucNangs.RemoveRange(quyen.PhanQuyenChucNangs);
                _context.Quyens.Remove(quyen);
                await _context.SaveChangesAsync();
            }
        }
    }
}
