using AutoMapper;
using HueFestivalAPI.DTO;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ChuongTrinh, ChuongTrinhDTO>()
                .ForMember(dest => dest.id_chuongtrinh, opt => opt.MapFrom(src => src.IdChuongTrinh))
                .ForMember(dest => dest.chuongtrinh_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.chuongtrinh_content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.type_inoff, opt => opt.MapFrom(src => src.TypeInOff))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.type_program, opt => opt.MapFrom(src => src.TypeProgram))
                .ForMember(dest => dest.arrange, opt => opt.MapFrom(src => src.Arrange))
                .ForMember(dest => dest.md5, opt => opt.MapFrom(src => src.Md5))
                .ForMember(dest => dest.pathimage_list, opt => opt.MapFrom(src => src.ChuongTrinhImages))
                .ForMember(dest => dest.details_list, opt => opt.MapFrom(src => src.ChuongTrinhDetails));
            CreateMap<ChuongTrinhImage, ChuongTrinhImageDTO>()
                .ForMember(dest => dest.id_pathimage, opt => opt.MapFrom(src => src.IdImage))
                .ForMember(dest => dest.pathimage, opt => opt.MapFrom(src => src.PathImage))
                .ForMember(dest => dest.id_chuongtrinh, opt => opt.MapFrom(src => src.IdChuongTrinh))
                .ForMember(dest => dest.ChuongTrinh, opt => opt.MapFrom(src => src.ChuongTrinh));
            CreateMap<ChuongTrinhDetails, ChuongTrinhDetailsDTO>()
                .ForMember(dest => dest.time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.fdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.tdate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.id_diadiem, opt => opt.MapFrom(src => src.IdDiaDiem))
                .ForMember(dest => dest.id_nhom, opt => opt.MapFrom(src => src.IdNhom))
                .ForMember(dest => dest.id_doan, opt => opt.MapFrom(src => src.IdDoan))
                .ForMember(dest => dest.diadiem_name, opt => opt.MapFrom(src => src.DiaDiemName))
                .ForMember(dest => dest.nhom_name, opt => opt.MapFrom(src => src.NhomName))
                .ForMember(dest => dest.doan_name, opt => opt.MapFrom(src => src.DoanName));
            CreateMap<ChuongTrinh, ChuongTrinhTheoNgayDTO>()
                .ForMember(dest => dest.md5, opt => opt.MapFrom(src => src.Md5))
                .ForMember(dest => dest.details_list, opt => opt.MapFrom(src => src.ChuongTrinhDetails));
            CreateMap<ChuongTrinhDetails, ChuongTrinhDetailsTheoNgayDTO>()
                .ForMember(dest => dest.time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.fdate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.tdate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.id_diadiem, opt => opt.MapFrom(src => src.IdDiaDiem))
                .ForMember(dest => dest.diadiem_name, opt => opt.MapFrom(src => src.DiaDiemName));
            CreateMap<DiaDiemMenu, DiaDiemMenuDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdMenu))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.pathicon, opt => opt.MapFrom(src => src.PathIcon))
                .ForMember(dest => dest.typedata, opt => opt.MapFrom(src => src.TypeData))
                .ForMember(dest => dest.submenu, opt => opt.MapFrom(src => src.DiaDiemSubMenus));
            CreateMap<DiaDiemSubMenu, DiaDiemSubMenuDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdSubMenu))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.pathicon, opt => opt.MapFrom(src => src.PathIcon))
                .ForMember(dest => dest.ptypeid, opt => opt.MapFrom(src => src.IdMenu))
                .ForMember(dest => dest.typedata, opt => opt.MapFrom(src => src.TypeData));
            CreateMap<DiaDiem, DiaDiemDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdDiaDiem))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.pathimage, opt => opt.MapFrom(src => src.PathImage))
                .ForMember(dest => dest.longtitude, opt => opt.MapFrom(src => src.Longtitude))
                .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.typedata, opt => opt.MapFrom(src => src.TypeData));
            CreateMap<DiaDiem, ChiTietDiaDiemDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdDiaDiem))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.pathimage, opt => opt.MapFrom(src => src.PathImage))
                .ForMember(dest => dest.postdate, opt => opt.MapFrom(src => src.PostDate))
                .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.longtitude, opt => opt.MapFrom(src => src.Longtitude));
            CreateMap<TinTuc, TinTucDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdTinTuc))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.pathimage, opt => opt.MapFrom(src => src.PathImage))
                .ForMember(dest => dest.summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.postdate, opt => opt.MapFrom(src => src.PostDate))
                .ForMember(dest => dest.changedate, opt => opt.MapFrom(src => src.ChangeDate));
            CreateMap<TinTuc, ChiTietTinTucDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdTinTuc))
                .ForMember(dest => dest.typeid, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.other_typeid, opt => opt.MapFrom(src => src.OtherTypeId))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.keywords, opt => opt.MapFrom(src => src.Keywords))
                .ForMember(dest => dest.summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.pathfile, opt => opt.MapFrom(src => src.PathFile))
                .ForMember(dest => dest.pathimage, opt => opt.MapFrom(src => src.PathImage))
                .ForMember(dest => dest.video, opt => opt.MapFrom(src => src.Video))
                .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.postdate, opt => opt.MapFrom(src => src.PostDate))
                .ForMember(dest => dest.changedate, opt => opt.MapFrom(src => src.ChangeDate))
                .ForMember(dest => dest.approved, opt => opt.MapFrom(src => src.Approved))
                .ForMember(dest => dest.isnew, opt => opt.MapFrom(src => src.IsNew))
                .ForMember(dest => dest.isfocus, opt => opt.MapFrom(src => src.IsFocus))
                .ForMember(dest => dest.ishome, opt => opt.MapFrom(src => src.IsHome))
                .ForMember(dest => dest.view, opt => opt.MapFrom(src => src.View))
                .ForMember(dest => dest.arrange, opt => opt.MapFrom(src => src.Arrange))
                .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.longtitude, opt => opt.MapFrom(src => src.Longtitude));
            CreateMap<MenuHoTro, MenuHoTroDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdHoTro))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title));
            CreateMap<MenuHoTro, ChiTietMenuHoTroDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdHoTro))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.content, opt => opt.MapFrom(src => src.Content));
        }
    }
}
