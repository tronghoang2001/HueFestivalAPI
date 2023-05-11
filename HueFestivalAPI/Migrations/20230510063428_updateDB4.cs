using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PTypeId",
                table: "DiaDiemSubMenu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PTypeId",
                table: "DiaDiemSubMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
