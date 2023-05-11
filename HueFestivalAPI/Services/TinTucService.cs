using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Services
{
    public class TinTucService : ITinTucService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public TinTucService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TinTucDTO>> GetAllTinTucAsync()
        {
            var tintucs = await _context.TinTucs
                .ToListAsync();
            return tintucs.Select(t => new TinTucDTO
            {
                id = t.IdTinTuc,
                title = t.Title,
                pathimage = t.PathImage,
                summary = t.Summary,
                postdate = t.PostDate,
                changedate = t.ChangeDate
            }).ToList();
        }

        public async Task<ChiTietTinTucDTO> GetTinTucByIdAsync(int id)
        {
            var tintuc = await _context.TinTucs
            .FirstOrDefaultAsync(t => t.IdTinTuc == id);

            if (tintuc == null)
            {
                return null;
            }

            var tintucDto = new ChiTietTinTucDTO
            {
                id = tintuc.IdTinTuc,
                typeid = tintuc.TypeId,
                other_typeid = tintuc.OtherTypeId,
                title = tintuc.Title,
                url = tintuc.Url,
                keywords = tintuc.Keywords,
                summary = tintuc.Summary,
                content = tintuc.Content,
                pathfile = tintuc.PathFile,
                pathimage = tintuc.PathImage,
                video = tintuc.Video,
                comment = tintuc.Comment,
                postdate = tintuc.PostDate,
                changedate = tintuc.ChangeDate,
                approved = tintuc.Approved,
                lang = "vn",
                author = "",
                isnew = tintuc.IsNew,
                isfocus = tintuc.IsFocus,
                ishome = tintuc.IsHome,
                view = tintuc.View,
                arrange = tintuc.Arrange,
                latitude = tintuc.Latitude,
                longtitude = tintuc.Longtitude
            };

            return tintucDto;
        }

        public async Task<TinTuc> AddTinTucAsync(AddTinTucDTO tinTucDto)
        {
            var tintuc = new TinTuc
            {
                TypeId = tinTucDto.TypeId,
                OtherTypeId = 0,
                Title = tinTucDto.Title,
                Summary = tinTucDto.Summary,
                Content = tinTucDto.Content,
                PathImage = tinTucDto.PathImage,
                PostDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Approved = tinTucDto.Approved,
                View = 0,
                Arrange = tinTucDto.Arrange,
                Latitude = tinTucDto.Latitude,
                Longtitude = tinTucDto.Longtitude,
                IdAccount = 4
            };

            await _context.TinTucs.AddAsync(tintuc);
            await _context.SaveChangesAsync();

            return tintuc;
        }

        public async Task<TinTuc> UpdateTinTucAsync(AddTinTucDTO tinTucDto, int id)
        {
            var tintuc = new TinTuc
            {
                IdTinTuc = id,
                TypeId = tinTucDto.TypeId,
                OtherTypeId = 0,
                Title = tinTucDto.Title,
                Summary = tinTucDto.Summary,
                Content = tinTucDto.Content,
                PathImage = tinTucDto.PathImage,
                PostDate = DateTime.Now,
                ChangeDate = DateTime.Now,
                Approved = tinTucDto.Approved,
                Arrange = tinTucDto.Arrange,
                Latitude = tinTucDto.Latitude,
                Longtitude = tinTucDto.Longtitude,
                IdAccount = 4
            };

            _context.TinTucs.Update(tintuc);
            await _context.SaveChangesAsync();
            return tintuc;
        }

        public async Task DeleteTinTucAsync(int id)
        {
            var tintuc = await _context.TinTucs
                .FirstOrDefaultAsync(c => c.IdTinTuc == id);

            if (tintuc != null)
            {
                _context.TinTucs.Remove(tintuc);
                await _context.SaveChangesAsync();
            }
        }

    }
}
