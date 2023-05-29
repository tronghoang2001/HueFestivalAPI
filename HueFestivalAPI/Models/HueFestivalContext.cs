using Microsoft.EntityFrameworkCore;

namespace HueFestivalAPI.Models
{
    public class HueFestivalContext : DbContext
    {
        public HueFestivalContext(DbContextOptions<HueFestivalContext> options) : base(options)
        {

        }
        #region DbSet
        public DbSet<Account> Account { get; set; }
        public DbSet<ChuongTrinh> ChuongTrinhs { get; set; }
        public DbSet<ChuongTrinhDetails> ChuongTrinhDetails { get; set; }
        public DbSet<ChuongTrinhImage> ChuongTrinhImages { get; set; }
        public DbSet<DiaDiem> DiaDiems { get; set; }
        public DbSet<DiaDiemMenu> DiaDiemMenus { get; set; }
        public DbSet<DiaDiemSubMenu> DiaDiemSubMenus { get; set; }
        public DbSet<DoanChuongTrinh> DoanChuongTrinhs { get; set; }
        public DbSet<MenuHoTro> MenuHoTros { get; set; }
        public DbSet<NhomChuongTrinh> NhomChuongTrinhs { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        public DbSet<ThongTinDatVe> HoaDons { get; set; }
        public DbSet<LoaiVe> LoaiVes { get; set; }
        public DbSet<PhanQuyenChucNang> PhanQuyenChucNangs { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<Ve> Ves { get; set; }
        public DbSet<KichHoatVe> KichHoatVes { get; set; }
        public DbSet<ThongTinDatVe> ThongTinDatVes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhanQuyenChucNang>()
                .HasKey(k => new { k.IdQuyen, k.IdChucNang });
            modelBuilder.Entity<TinTuc>()
                .HasIndex(c => c.Title)
                .IsUnique();
            modelBuilder.Entity<NhomChuongTrinh>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Account>()
                .HasIndex(a => new { a.Email, a.PhoneNumber })
                .IsUnique();
            modelBuilder.Entity<MenuHoTro>()
                .HasIndex(c => c.Title)
                .IsUnique();
            modelBuilder.Entity<DiaDiem>()
                .HasIndex(c => c.Title)
                .IsUnique();
            modelBuilder.Entity<DiaDiemSubMenu>()
                .HasIndex(c => c.Title)
                .IsUnique();
            modelBuilder.Entity<DiaDiemMenu>()
                .HasIndex(c => c.Title)
                .IsUnique();
            modelBuilder.Entity<ChuongTrinh>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Quyen>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<ChucNang>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<LoaiVe>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<ChuongTrinhDetails>()
                .HasOne(c => c.ChuongTrinh)
                .WithMany(ct => ct.ChuongTrinhDetails)
                .HasForeignKey(c => c.IdChuongTrinh);
            modelBuilder.Entity<ChuongTrinhDetails>()
                .HasOne(c => c.DiaDiem)
                .WithMany(ct => ct.ChuongTrinhDetails)
                .HasForeignKey(c => c.IdDiaDiem);
            modelBuilder.Entity<ChuongTrinhDetails>()
                .HasOne(c => c.NhomChuongTrinh)
                .WithMany(ct => ct.ChuongTrinhDetails)
                .HasForeignKey(c => c.IdNhom);
            modelBuilder.Entity<ChuongTrinhDetails>()
                .HasOne(c => c.DoanChuongTrinh)
                .WithMany(ct => ct.ChuongTrinhDetails)
                .HasForeignKey(c => c.IdDoan);
            modelBuilder.Entity<ChuongTrinhImage>()
                .HasOne(c => c.ChuongTrinh)
                .WithMany(ct => ct.ChuongTrinhImages)
                .HasForeignKey(c => c.IdChuongTrinh);
            modelBuilder.Entity<DiaDiem>()
                .HasOne(c => c.Account)
                .WithMany(ct => ct.DiaDiems)
                .HasForeignKey(c => c.IdAccount);
            modelBuilder.Entity<DiaDiem>()
                .HasOne(c => c.DiaDiemSubMenu)
                .WithMany(ct => ct.DiaDiems)
                .HasForeignKey(c => c.IdSubMenu);
            modelBuilder.Entity<DiaDiemSubMenu>()
                .HasOne(c => c.DiaDiemMenu)
                .WithMany(ct => ct.DiaDiemSubMenus)
                .HasForeignKey(c => c.IdMenu);
            modelBuilder.Entity<TinTuc>()
                .HasOne(c => c.Account)
                .WithMany(ct => ct.TinTucs)
                .HasForeignKey(c => c.IdAccount);
            modelBuilder.Entity<Checkin>()
                .HasOne(c => c.KichHoatVe)
                .WithMany(ct => ct.Checkins)
                .HasForeignKey(c => c.IdKichHoat);
            modelBuilder.Entity<ThongTinDatVe>()
                .HasOne(c => c.Ve)
                .WithMany(ct => ct.ThongTinDatVes)
                .HasForeignKey(c => c.IdVe);
            modelBuilder.Entity<PhanQuyenChucNang>()
                .HasOne(c => c.Quyen)
                .WithMany(ct => ct.PhanQuyenChucNangs)
                .HasForeignKey(c => c.IdQuyen);
            modelBuilder.Entity<PhanQuyenChucNang>()
                .HasOne(c => c.ChucNang)
                .WithMany(ct => ct.PhanQuyenChucNangs)
                .HasForeignKey(c => c.IdChucNang);
            modelBuilder.Entity<Ve>()
                .HasOne(c => c.ChuongTrinhDetails)
                .WithMany(ct => ct.Ves)
                .HasForeignKey(c => c.IdDetails);
            modelBuilder.Entity<Ve>()
                .HasOne(c => c.LoaiVe)
                .WithMany(ct => ct.Ves)
                .HasForeignKey(c => c.IdLoaiVe);
            modelBuilder.Entity<Account>()
               .HasOne(c => c.Quyen)
               .WithMany(ct => ct.Accounts)
               .HasForeignKey(c => c.IdQuyen);
            modelBuilder.Entity<KichHoatVe>()
               .HasOne(c => c.ThongTinDatVe)
               .WithMany(ct => ct.KichHoatVes)
               .HasForeignKey(c => c.IdThongTin);
        }
    }
}
