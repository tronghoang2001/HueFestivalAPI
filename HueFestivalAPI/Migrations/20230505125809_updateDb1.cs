using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdQuyen",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuyenIdQuyen",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChucNang",
                columns: table => new
                {
                    IdChucNang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucNang", x => x.IdChucNang);
                });

            migrationBuilder.CreateTable(
                name: "DiemBanVe",
                columns: table => new
                {
                    IdDiemBanVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemBanVe", x => x.IdDiemBanVe);
                });

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

            migrationBuilder.CreateTable(
                name: "LoaiVe",
                columns: table => new
                {
                    IdLoaiVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiVe", x => x.IdLoaiVe);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    IdQuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyen", x => x.IdQuyen);
                });

            migrationBuilder.CreateTable(
                name: "Ve",
                columns: table => new
                {
                    IdVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayPhatHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDetails = table.Column<int>(type: "int", nullable: false),
                    IdLoaiVe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ve", x => x.IdVe);
                    table.ForeignKey(
                        name: "FK_Ve_ChuongTrinhDetails_IdDetails",
                        column: x => x.IdDetails,
                        principalTable: "ChuongTrinhDetails",
                        principalColumn: "IdDetails",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ve_LoaiVe_IdLoaiVe",
                        column: x => x.IdLoaiVe,
                        principalTable: "LoaiVe",
                        principalColumn: "IdLoaiVe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyenChucNang",
                columns: table => new
                {
                    IdQuyen = table.Column<int>(type: "int", nullable: false),
                    IdChucNang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyenChucNang", x => new { x.IdQuyen, x.IdChucNang });
                    table.ForeignKey(
                        name: "FK_PhanQuyenChucNang_ChucNang_IdChucNang",
                        column: x => x.IdChucNang,
                        principalTable: "ChucNang",
                        principalColumn: "IdChucNang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanQuyenChucNang_Quyen_IdQuyen",
                        column: x => x.IdQuyen,
                        principalTable: "Quyen",
                        principalColumn: "IdQuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDiemBanVe",
                columns: table => new
                {
                    IdChiTietDiemBanVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    IdDiemBanVe = table.Column<int>(type: "int", nullable: false),
                    IdVe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDiemBanVe", x => x.IdChiTietDiemBanVe);
                    table.ForeignKey(
                        name: "FK_ChiTietDiemBanVe_DiemBanVe_IdDiemBanVe",
                        column: x => x.IdDiemBanVe,
                        principalTable: "DiemBanVe",
                        principalColumn: "IdDiemBanVe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDiemBanVe_Ve_IdVe",
                        column: x => x.IdVe,
                        principalTable: "Ve",
                        principalColumn: "IdVe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    IdHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKhachHang = table.Column<int>(type: "int", nullable: false),
                    IdChiTietDiemBanVe = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Checkin",
                columns: table => new
                {
                    IdCheckin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCheckin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdHoaDon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkin", x => x.IdCheckin);
                    table.ForeignKey(
                        name: "FK_Checkin_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "IdHoaDon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_QuyenIdQuyen",
                table: "Account",
                column: "QuyenIdQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_IdHoaDon",
                table: "Checkin",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDiemBanVe_IdDiemBanVe",
                table: "ChiTietDiemBanVe",
                column: "IdDiemBanVe");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDiemBanVe_IdVe",
                table: "ChiTietDiemBanVe",
                column: "IdVe");

            migrationBuilder.CreateIndex(
                name: "IX_ChucNang_Name",
                table: "ChucNang",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiemBanVe_Name",
                table: "DiemBanVe",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdChiTietDiemBanVe",
                table: "HoaDon",
                column: "IdChiTietDiemBanVe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdKhachHang",
                table: "HoaDon",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiVe_Name",
                table: "LoaiVe",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyenChucNang_IdChucNang",
                table: "PhanQuyenChucNang",
                column: "IdChucNang");

            migrationBuilder.CreateIndex(
                name: "IX_Quyen_Name",
                table: "Quyen",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ve_IdDetails",
                table: "Ve",
                column: "IdDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_IdLoaiVe",
                table: "Ve",
                column: "IdLoaiVe");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Quyen_QuyenIdQuyen",
                table: "Account",
                column: "QuyenIdQuyen",
                principalTable: "Quyen",
                principalColumn: "IdQuyen",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Quyen_QuyenIdQuyen",
                table: "Account");

            migrationBuilder.DropTable(
                name: "Checkin");

            migrationBuilder.DropTable(
                name: "PhanQuyenChucNang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ChucNang");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "ChiTietDiemBanVe");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "DiemBanVe");

            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "LoaiVe");

            migrationBuilder.DropIndex(
                name: "IX_Account_QuyenIdQuyen",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "IdQuyen",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "QuyenIdQuyen",
                table: "Account");
        }
    }
}
