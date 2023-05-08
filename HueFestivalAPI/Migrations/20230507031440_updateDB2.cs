using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaDiemName",
                table: "ChuongTrinhDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoanName",
                table: "ChuongTrinhDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhomName",
                table: "ChuongTrinhDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Md5",
                table: "ChuongTrinh",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDiemName",
                table: "ChuongTrinhDetails");

            migrationBuilder.DropColumn(
                name: "DoanName",
                table: "ChuongTrinhDetails");

            migrationBuilder.DropColumn(
                name: "NhomName",
                table: "ChuongTrinhDetails");

            migrationBuilder.DropColumn(
                name: "Md5",
                table: "ChuongTrinh");
        }
    }
}
