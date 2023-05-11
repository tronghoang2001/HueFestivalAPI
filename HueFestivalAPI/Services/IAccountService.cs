using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services
{
    public interface IAccountService
    {
        public Task<Account> AddAccountAsync(AccountDTO accountDto);
        public Task<string> LoginAsync(LoginDTO loginDto);
    }
}
