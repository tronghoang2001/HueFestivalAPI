using AutoMapper;
using HueFestivalAPI.DTO.Ve;
using HueFestivalAPI.Models;
using HueFestivalAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Cryptography;
using System.Text;

namespace HueFestivalAPI.Services
{
    public class VeService : IVeService
    {
        private readonly HueFestivalContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public VeService(HueFestivalContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration; 
        }

        public async Task<Ve> PhatHanhVeAsync(AddVeDTO veDto, int idDetails)
        {
            var ve = _mapper.Map<Ve>(veDto);
            ve.NgayPhatHanh = DateTime.Now;
            ve.IdDetails = idDetails;
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

        public async Task<ThongTinDatVe> AddThongTinDatVe(ThongTinDatVeDTO thongTinDatVeDTO)
        {
            var ve = _context.Ves.FirstOrDefault(v => v.IdVe == thongTinDatVeDTO.IdVe);
            var thongTinDatVe = _mapper.Map<ThongTinDatVe>(thongTinDatVeDTO);
            if (ve == null || ve.SoLuong < thongTinDatVeDTO.SoLuong)
            {
                return null;
            }

            var giaVe = ve.GiaVe;
            var tongTien = thongTinDatVeDTO.SoLuong * giaVe;

            thongTinDatVe.TongTien = tongTien;

            ve.SoLuong -= thongTinDatVeDTO.SoLuong;
            _context.SaveChanges();

            _context.ThongTinDatVes.Add(thongTinDatVe);
            await _context.SaveChangesAsync();

            return thongTinDatVe;
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

        public async Task<List<string>> ThanhToanAsync(ThongTinThanhToanDTO thongTinThanhToanDTO, int idThongTin)
        {
            var stripeSecretKey = _configuration["StripeSettings:SecretKey"];
            StripeConfiguration.ApiKey = stripeSecretKey;

            // Tạo một token thanh toán trên máy chủ sử dụng Stripe.NET SDK hoặc thực hiện yêu cầu API trực tiếp
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = thongTinThanhToanDTO.Number,
                    ExpMonth = thongTinThanhToanDTO.ExpMonth.ToString(),
                    ExpYear = thongTinThanhToanDTO.ExpYear.ToString(),
                    Cvc = thongTinThanhToanDTO.Cvc
                }
            };

            var tokenService = new TokenService();
            Token stripeToken = await tokenService.CreateAsync(tokenOptions);

            var stripeTokenId = stripeToken.Id;
            var thongTinDatVe = _context.ThongTinDatVes.FirstOrDefault(v => v.IdThongTin == idThongTin);

            var options = new ChargeCreateOptions
            {
                Amount = (long?)thongTinDatVe.TongTien,
                Currency = "vnd", // Đơn vị tiền tệ là VND
                Source = stripeTokenId // Sử dụng token từ Stripe
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Status == "succeeded")
            {
                // Xử lý khi thanh toán thành công
                if (thongTinDatVe != null)
                {
                    thongTinDatVe.TinhTrangThanhToan = true;
                    _context.SaveChanges();
                    List<string> qrCodes = new List<string>();

                    for (int i = 0; i < thongTinDatVe.SoLuong; i++)
                    {
                        var kichHoatVe = new KichHoatVe
                        {
                            IdThongTin = thongTinDatVe.IdThongTin,
                            NgayKichHoat = DateTime.Now,
                            QRCode = CalculateQRCode(thongTinDatVe.IdThongTin, i + 1)
                        };

                        _context.KichHoatVes.Add(kichHoatVe);
                        qrCodes.Add(kichHoatVe.QRCode);
                    }
                    _context.SaveChanges();
                    return qrCodes;
                }
            }
            else
            {
                throw new Exception("Thanh toán thất bại!");
            }
            return null;
        }

        public async Task<object> CheckinAsync(string qrCode)
        {
            var checkQrCode = await _context.KichHoatVes.FirstOrDefaultAsync(a => a.QRCode == qrCode);
            if (checkQrCode == null)
            {
                return null;
            }

            var existingCheckin = await _context.Checkins.FirstOrDefaultAsync(c => c.IdKichHoat == checkQrCode.IdKichHoat);
            if (existingCheckin != null)
            {
                return null;
            }

            var checkin = new Checkin
            {
                DateCheckin = DateTime.Now,
                IdKichHoat = checkQrCode.IdKichHoat,
            };

            _context.Checkins.Add(checkin);
            await _context.SaveChangesAsync();

            var thongTinCheckin = await _context.Checkins
                .Include(c => c.KichHoatVe)
                    .ThenInclude(c => c.ThongTinDatVe)
                    .ThenInclude(c => c.Ve)
                    .ThenInclude(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.ChuongTrinh)
                .Include(c => c.KichHoatVe)
                    .ThenInclude(c => c.ThongTinDatVe)
                    .ThenInclude(c => c.Ve)
                    .ThenInclude(c => c.ChuongTrinhDetails)
                    .ThenInclude(c => c.DiaDiem)
                .Include(c => c.KichHoatVe)
                    .ThenInclude(c => c.ThongTinDatVe)
                    .ThenInclude(c => c.Ve)
                    .ThenInclude(c => c.LoaiVe)
                .FirstOrDefaultAsync(c => c.IdCheckin == checkin.IdCheckin);

            var checkinDto = _mapper.Map<ThongTinCheckinDTO>(thongTinCheckin);
            var result = new
            {
                message = "Thông tin vé hợp lệ. Checkin thành công!",
                detail = checkinDto
            };
            return result;
        }

    }
}
