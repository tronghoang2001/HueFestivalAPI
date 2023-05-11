﻿using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Services
{
    public interface ITinTucService
    {
        public Task<List<TinTucDTO>> GetAllTinTucAsync();
        public Task<ChiTietTinTucDTO> GetTinTucByIdAsync(int id);
        public Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto);
        public Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id);
        public Task DeleteTinTucAsync(int id);
    }
}
