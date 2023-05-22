using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class updateDB9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDiemBanVe");

            migrationBuilder.DropTable(
                name: "DiemBanVe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiemBanVe",
                columns: table => new
                {
                    IdDiemBanVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemBanVe", x => x.IdDiemBanVe);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDiemBanVe",
                columns: table => new
                {
                    IdChiTietDiemBanVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDiemBanVe = table.Column<int>(type: "int", nullable: false),
                    IdVe = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDiemBanVe_IdDiemBanVe",
                table: "ChiTietDiemBanVe",
                column: "IdDiemBanVe");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDiemBanVe_IdVe",
                table: "ChiTietDiemBanVe",
                column: "IdVe");

            migrationBuilder.CreateIndex(
                name: "IX_DiemBanVe_Name",
                table: "DiemBanVe",
                column: "Name",
                unique: true);
        }
    }
}
