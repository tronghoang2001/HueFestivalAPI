using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
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

        public AccountService(HueFestivalContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Account> AddAccountAsync(AccountDTO accountDto)
        {
            var account = new Account
            {
                FullName = accountDto.FullName,
                Email = accountDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(accountDto.Password),
                PhoneNumber = accountDto.PhoneNumber,
                Role = "User",
                Status = true,
                IdQuyen = 3,
            };

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
    }
}
