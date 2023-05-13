using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinDatVe_KhachHang_KhachHangIdKhachHang",
                table: "ThongTinDatVe");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropIndex(
                name: "IX_ThongTinDatVe_KhachHangIdKhachHang",
                table: "ThongTinDatVe");

            migrationBuilder.DropColumn(
                name: "KhachHangIdKhachHang",
                table: "ThongTinDatVe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KhachHangIdKhachHang",
                table: "ThongTinDatVe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    IdKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoCMND = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.IdKhachHang);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatVe_KhachHangIdKhachHang",
                table: "ThongTinDatVe",
                column: "KhachHangIdKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinDatVe_KhachHang_KhachHangIdKhachHang",
                table: "ThongTinDatVe",
                column: "KhachHangIdKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IdKhachHang");
        }
    }
}
