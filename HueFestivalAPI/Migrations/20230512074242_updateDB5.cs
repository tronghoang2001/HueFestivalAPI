using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_HoaDon_IdHoaDon",
                table: "Checkin");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "IdHoaDon",
                table: "Checkin",
                newName: "IdThongTin");

            migrationBuilder.RenameIndex(
                name: "IX_Checkin_IdHoaDon",
                table: "Checkin",
                newName: "IX_Checkin_IdThongTin");

            migrationBuilder.CreateTable(
                name: "ThongTinDatVe",
                columns: table => new
                {
                    IdThongTin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoCMND = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdVe = table.Column<int>(type: "int", nullable: false),
                    ChiTietDiemBanVeIdChiTietDiemBanVe = table.Column<int>(type: "int", nullable: true),
                    KhachHangIdKhachHang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinDatVe", x => x.IdThongTin);
                    table.ForeignKey(
                        name: "FK_ThongTinDatVe_ChiTietDiemBanVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                        column: x => x.ChiTietDiemBanVeIdChiTietDiemBanVe,
                        principalTable: "ChiTietDiemBanVe",
                        principalColumn: "IdChiTietDiemBanVe");
                    table.ForeignKey(
                        name: "FK_ThongTinDatVe_KhachHang_KhachHangIdKhachHang",
                        column: x => x.KhachHangIdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IdKhachHang");
                    table.ForeignKey(
                        name: "FK_ThongTinDatVe_Ve_IdVe",
                        column: x => x.IdVe,
                        principalTable: "Ve",
                        principalColumn: "IdVe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe",
                column: "ChiTietDiemBanVeIdChiTietDiemBanVe");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatVe_IdVe",
                table: "ThongTinDatVe",
                column: "IdVe");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatVe_KhachHangIdKhachHang",
                table: "ThongTinDatVe",
                column: "KhachHangIdKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_ThongTinDatVe_IdThongTin",
                table: "Checkin",
                column: "IdThongTin",
                principalTable: "ThongTinDatVe",
                principalColumn: "IdThongTin",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_ThongTinDatVe_IdThongTin",
                table: "Checkin");

            migrationBuilder.DropTable(
                name: "ThongTinDatVe");

            migrationBuilder.RenameColumn(
                name: "IdThongTin",
                table: "Checkin",
                newName: "IdHoaDon");

            migrationBuilder.RenameIndex(
                name: "IX_Checkin_IdThongTin",
                table: "Checkin",
                newName: "IX_Checkin_IdHoaDon");

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    IdHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChiTietDiemBanVe = table.Column<int>(type: "int", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.IdHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_ChiTietDiemBanVe_IdChiTietDiemBanVe",
                        column: x => x.IdChiTietDiemBanVe,
                        principalTable: "ChiTietDiemBanVe",
                        principalColumn: "IdChiTietDiemBanVe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdChiTietDiemBanVe",
                table: "HoaDon",
                column: "IdChiTietDiemBanVe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdKhachHang",
                table: "HoaDon",
                column: "IdKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_HoaDon_IdHoaDon",
                table: "Checkin",
                column: "IdHoaDon",
                principalTable: "HoaDon",
                principalColumn: "IdHoaDon",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
