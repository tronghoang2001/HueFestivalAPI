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
    }
}
