using AutoMapper;
using HueFestivalAPI.DTO.Account;
using HueFestivalAPI.DTO.ChuongTrinh;
using HueFestivalAPI.DTO.DiaDiem;
using HueFestivalAPI.DTO.Doan;
using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.DTO.MenuHoTro;
using HueFestivalAPI.DTO.Nhom;
using HueFestivalAPI.DTO.TinTuc;
using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;

namespace HueFestivalAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ChuongTrinhDetails, ChuongTrinhDetailsDTO>()
                .ForMember(dest => dest.fdate, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.tdate, opt => opt.MapFrom(src => src.EndDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.id_doan, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.doan_name, opt => opt.NullSubstitute(null))
                .ForMember(dest => dest.id_diadiem, opt => opt.MapFrom(src => src.IdDiaDiem))
                .ForMember(dest => dest.diadiem_name, opt => opt.MapFrom(src => src.DiaDiem.Title))
                .ForMember(dest => dest.id_nhom, opt => opt.MapFrom(src => src.IdNhom))
                .ForMember(dest => dest.nhom_name, opt => opt.MapFrom(src => src.NhomChuongTrinh.Name));
            CreateMap<ChuongTrinh, ChuongTrinhDTO>()
                .ForMember(dest => dest.id_chuongtrinh, opt => opt.MapFrom(src => src.IdChuongTrinh))
                .ForMember(dest => dest.chuongtrinh_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.chuongtrinh_content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.type_inoff, opt => opt.MapFrom(src => src.TypeInOff))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.type_program, opt => opt.MapFrom(src => src.TypeProgram))
                .ForMember(dest => dest.arrange, opt => opt.MapFrom(src => src.Arrange))
                .ForMember(dest => dest.details_list, opt => opt.MapFrom(src => src.ChuongTrinhDetails))
                .ForMember(dest => dest.pathimage_list, opt => opt.MapFrom(src => src.ChuongTrinhImages.Select(i => i.PathImage)));
            CreateMap<AddChuongTrinhDTO, ChuongTrinh>();
            CreateMap<UpdateChuongTrinhDTO, ChuongTrinh>();
            CreateMap<UpdateChuongTrinhDetailsDTO, ChuongTrinhDetails>();

            CreateMap<AddAccountDTO, Account>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IdQuyen, opt => opt.MapFrom(src => 3));
            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdAccount))
                .ForMember(dest => dest.fullname, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.phonenumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));
            CreateMap<PhanQuyenChucNang, PhanQuyenChucNangDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.ChucNang.IdChucNang))
                .ForMember(dest => dest.chucnang_name, opt => opt.MapFrom(src => src.ChucNang.Name));
            CreateMap<Quyen, QuyenDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdQuyen))
                .ForMember(dest => dest.quyen_name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.chucnang_list, opt => opt.MapFrom(src => src.PhanQuyenChucNangs));

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
                .ForMember(dest => dest.lang, opt => opt.MapFrom(src => "vn"));
            CreateMap<AddDiaDiemMenuDTO, DiaDiemMenu>();
            CreateMap<AddDiaDiemSubMenuDTO, DiaDiemSubMenu>();
            CreateMap<AddDiaDiemDTO, DiaDiem>();

            CreateMap<MenuHoTro, MenuHoTroDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdHoTro));
            CreateMap<MenuHoTro, ChiTietMenuHoTroDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdHoTro));
            CreateMap<AddMenuHoTroDTO, MenuHoTro>();

            CreateMap<TinTuc, TinTucDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdTinTuc));
            CreateMap<TinTuc, ChiTietTinTucDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdTinTuc))
                .ForMember(dest => dest.other_typeid, opt => opt.MapFrom(src => src.OtherTypeId))
                .ForMember(dest => dest.lang, opt => opt.MapFrom(src => "vn"))
                .ForMember(dest => dest.author, opt => opt.MapFrom(src => ""));
            CreateMap<AddTinTucDTO, TinTuc>()
                .ForMember(dest => dest.OtherTypeId, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.View, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.IdAccount, opt => opt.MapFrom(src => 4));

            CreateMap<LoaiVe, LoaiVeDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdLoaiVe));
            CreateMap<AddLoaiVeDTO, LoaiVe>();
            CreateMap<AddVeDTO, Ve>();
            CreateMap<Ve, VeDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdVe))
                .ForMember(dest => dest.gia, opt => opt.MapFrom(src => src.GiaVe))
                .ForMember(dest => dest.loaive, opt => opt.MapFrom(src => src.LoaiVe.Name))
                .ForMember(dest => dest.chuongtrinh_name, opt => opt.MapFrom(src => src.ChuongTrinhDetails.ChuongTrinh.Name))
                .ForMember(dest => dest.diadiem_name, opt => opt.MapFrom(src => src.ChuongTrinhDetails.DiaDiem.Title));

            CreateMap<NhomChuongTrinh, NhomChuongTrinhDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdNhom));
            CreateMap<AddNhomDTO, NhomChuongTrinh>();

            CreateMap<DoanChuongTrinh, DoanChuongTrinhDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdDoan));
            CreateMap<AddDoanDTO, DoanChuongTrinh>();
        }
    }
}
