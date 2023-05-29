using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_ThongTinDatVe_IdThongTin",
                table: "Checkin");

            migrationBuilder.RenameColumn(
                name: "IdThongTin",
                table: "Checkin",
                newName: "IdKichHoat");

            migrationBuilder.RenameIndex(
                name: "IX_Checkin_IdThongTin",
                table: "Checkin",
                newName: "IX_Checkin_IdKichHoat");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_KichHoatVe_IdKichHoat",
                table: "Checkin",
                column: "IdKichHoat",
                principalTable: "KichHoatVe",
                principalColumn: "IdKichHoat",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_KichHoatVe_IdKichHoat",
                table: "Checkin");

            migrationBuilder.RenameColumn(
                name: "IdKichHoat",
                table: "Checkin",
                newName: "IdThongTin");

            migrationBuilder.RenameIndex(
                name: "IX_Checkin_IdKichHoat",
                table: "Checkin",
                newName: "IX_Checkin_IdThongTin");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_ThongTinDatVe_IdThongTin",
                table: "Checkin",
                column: "IdThongTin",
                principalTable: "ThongTinDatVe",
                principalColumn: "IdThongTin",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
