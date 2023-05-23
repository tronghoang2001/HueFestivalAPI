using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KichHoatVe",
                columns: table => new
                {
                    IdKichHoat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayKichHoat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdThongTin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichHoatVe", x => x.IdKichHoat);
                    table.ForeignKey(
                        name: "FK_KichHoatVe_ThongTinDatVe_IdThongTin",
                        column: x => x.IdThongTin,
                        principalTable: "ThongTinDatVe",
                        principalColumn: "IdThongTin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KichHoatVe_IdThongTin",
                table: "KichHoatVe",
                column: "IdThongTin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KichHoatVe");
        }
    }
}
