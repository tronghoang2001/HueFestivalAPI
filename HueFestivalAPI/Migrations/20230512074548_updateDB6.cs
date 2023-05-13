using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinDatVe_ChiTietDiemBanVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe");

            migrationBuilder.DropIndex(
                name: "IX_ThongTinDatVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe");

            migrationBuilder.DropColumn(
                name: "ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinDatVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe",
                column: "ChiTietDiemBanVeIdChiTietDiemBanVe");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinDatVe_ChiTietDiemBanVe_ChiTietDiemBanVeIdChiTietDiemBanVe",
                table: "ThongTinDatVe",
                column: "ChiTietDiemBanVeIdChiTietDiemBanVe",
                principalTable: "ChiTietDiemBanVe",
                principalColumn: "IdChiTietDiemBanVe");
        }
    }
}
