﻿using HueFestivalAPI.DTO.Account;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services.IServices
{
    public interface IAccountService
    {
        public Task<List<AccountDTO>> GetAllAccountAsync();
        public Task<Account> AddAccountAsync(AddAccountDTO accountDto);
        public Task<Account> AdminUpdateAccountAsync(AdminUpdateAccountDTO accountDto, int id);
        public Task<Account> UserUpdateAccountAsync(UserUpdateAccountDTO accountDto, int id);
        public Task<string> LoginAsync(LoginDTO loginDto);
        public Task<Account> ChangePasswordAsync(ChangePasswordDTO accountDto, string email);
        public Task<List<ChucNangDTO>> GetAllChucNangAsync();
        public Task<ChucNang> AddChucNangAsync(AddChucNangDTO chucNangDto);
        public Task<ChucNang> UpdateChucNangAsync(AddChucNangDTO chucNangDto, int id);
        public Task<bool> DeleteChucNangAsync(int id);
        public Task<List<QuyenDTO>> GetAllQuyenAsync();
        public Task<Quyen> AddQuyenAsync(AddQuyenDTO quyenDto);
        public Task<bool> DeleteQuyenAsync(int id);
        public Task<bool> ForgotPassword(string email);
        public Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDto, string email);
    }
}
