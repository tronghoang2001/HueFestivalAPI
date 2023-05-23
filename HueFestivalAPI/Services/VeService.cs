using AutoMapper;
using HueFestivalAPI.DTO.LoaiVe;
using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HueFestivalAPI.Services
{
    public class VeService : IVeService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;

        public VeService(HueFestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int id_details)
        {
            var ve = _mapper.Map<Ve>(veDto);
            ve.NgayPhatHanh = DateTime.Now;
            ve.IdDetails = id_details;
            await _context.Ves.AddAsync(ve);
            await _context.SaveChangesAsync();
            return ve;
        }

        public async Task<List<VeDTO>> GetAllVeAsync()
        {
            var ves = await _context.Ves
                .Include(c => c.LoaiVe)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.ChuongTrinh)
                .Include(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .ToListAsync();

            var veDtos = _mapper.Map<List<VeDTO>>(ves);

            return veDtos;
        }

        public List<string> CreateThongTinDatVeAsync(ThongTinDatVeDTO thongTinDatVeDTO)
        {
            // Kiểm tra số lượng vé còn lại
            var soLuongVeConLai = GetSoLuongVeConLai(thongTinDatVeDTO.IdVe); // Hàm GetSoLuongVeConLai() lấy số lượng vé còn lại từ IdVe

            if (thongTinDatVeDTO.SoLuong > soLuongVeConLai)
            {
                // Số lượng vé không đủ, trả về danh sách QRCode rỗng
                return new List<string>();
            }

            // Lấy giá vé từ IdVe
            var giaVe = _context.Ves.Where(v => v.IdVe == thongTinDatVeDTO.IdVe).Select(v => v.GiaVe).FirstOrDefault();

            // Tính tổng tiền
            var tongTien = thongTinDatVeDTO.SoLuong * giaVe;

            // Lưu thông tin vào bảng ThongTinDatVe
            var thongTinDatVe = new ThongTinDatVe
            {
                HoTen = thongTinDatVeDTO.HoTen,
                NgaySinh = DateTime.Parse(thongTinDatVeDTO.NgaySinh),
                SoCMND = thongTinDatVeDTO.SoCMND,
                SoDienThoai = thongTinDatVeDTO.SoDienThoai,
                SoLuong = thongTinDatVeDTO.SoLuong,
                TongTien = tongTien, // Cập nhật tổng tiền
                NgayDat = DateTime.Now,
                TinhTrangThanhToan = false,
                IdVe = thongTinDatVeDTO.IdVe
            };

            _context.ThongTinDatVes.Add(thongTinDatVe);
            _context.SaveChanges();

            // Lưu thông tin vào bảng KichHoatVe
            List<string> qrCodes = new List<string>();

            for (int i = 0; i < thongTinDatVeDTO.SoLuong; i++)
            {
                var kichHoatVe = new KichHoatVe
                {
                    IdThongTin = thongTinDatVe.IdThongTin,
                    NgayKichHoat = DateTime.Now,
                    QRCode = CalculateQRCode(thongTinDatVe.IdThongTin, i + 1) // Hàm CalculateQRCode() tính toán và trả về QRCode
                };

                _context.KichHoatVes.Add(kichHoatVe);
                qrCodes.Add(kichHoatVe.QRCode);
            }

            _context.SaveChanges();

            return qrCodes;
        }


        private int GetSoLuongVeConLai(int idVe)
        {
            // Lấy số lượng vé đã được đặt từ bảng ThongTinDatVe
            var soLuongDaDat = _context.ThongTinDatVes
                .Where(t => t.IdVe == idVe)
                .Sum(t => t.SoLuong);

            // Lấy số lượng vé ban đầu từ bảng Ve
            var ve = _context.Ves.FirstOrDefault(v => v.IdVe == idVe);
            var soLuongBanDau = ve != null ? ve.SoLuong : 0;

            // Tính số lượng vé còn lại
            var soLuongConLai = soLuongBanDau - soLuongDaDat;

            return soLuongConLai;
        }

        private string CalculateQRCode(int idThongTin, int idKichHoat)
        {
            var combinedString = $"{idKichHoat}{idThongTin}";

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
