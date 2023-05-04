﻿using Microsoft.EntityFrameworkCore;

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
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
