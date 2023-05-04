using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalAPI.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.IdAccount);
                });

            migrationBuilder.CreateTable(
                name: "ChuongTrinh",
                columns: table => new
                {
                    IdChuongTrinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeInOff = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeProgram = table.Column<int>(type: "int", nullable: false),
                    Arrange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinh", x => x.IdChuongTrinh);
                });

            migrationBuilder.CreateTable(
                name: "DiaDiemMenu",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PathIcon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeData = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaDiemMenu", x => x.IdMenu);
                });

            migrationBuilder.CreateTable(
                name: "DoanChuongTrinh",
                columns: table => new
                {
                    IdDoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanChuongTrinh", x => x.IdDoan);
                });

            migrationBuilder.CreateTable(
                name: "MenuHoTro",
                columns: table => new
                {
                    IdHoTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuHoTro", x => x.IdHoTro);
                });

            migrationBuilder.CreateTable(
                name: "NhomChuongTrinh",
                columns: table => new
                {
                    IdNhom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomChuongTrinh", x => x.IdNhom);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    IdTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    OtherTypeId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PathImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Video = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved = table.Column<int>(type: "int", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: true),
                    IsFocus = table.Column<bool>(type: "bit", nullable: true),
                    IsHome = table.Column<bool>(type: "bit", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Arrange = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<int>(type: "int", nullable: false),
                    Longtitude = table.Column<int>(type: "int", nullable: false),
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinTuc", x => x.IdTinTuc);
                    table.ForeignKey(
                        name: "FK_TinTuc_Account_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChuongTrinhImage",
                columns: table => new
                {
                    IdImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdChuongTrinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinhImage", x => x.IdImage);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhImage_ChuongTrinh_IdChuongTrinh",
                        column: x => x.IdChuongTrinh,
                        principalTable: "ChuongTrinh",
                        principalColumn: "IdChuongTrinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaDiemSubMenu",
                columns: table => new
                {
                    IdSubMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PathIcon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeData = table.Column<int>(type: "int", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaDiemSubMenu", x => x.IdSubMenu);
                    table.ForeignKey(
                        name: "FK_DiaDiemSubMenu_DiaDiemMenu_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "DiaDiemMenu",
                        principalColumn: "IdMenu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaDiem",
                columns: table => new
                {
                    IdDiaDiem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Longtitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    TypeData = table.Column<int>(type: "int", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdAccount = table.Column<int>(type: "int", nullable: false),
                    IdSubMenu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaDiem", x => x.IdDiaDiem);
                    table.ForeignKey(
                        name: "FK_DiaDiem_Account_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaDiem_DiaDiemSubMenu_IdSubMenu",
                        column: x => x.IdSubMenu,
                        principalTable: "DiaDiemSubMenu",
                        principalColumn: "IdSubMenu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChuongTrinhDetails",
                columns: table => new
                {
                    IdDetails = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdChuongTrinh = table.Column<int>(type: "int", nullable: false),
                    IdDiaDiem = table.Column<int>(type: "int", nullable: false),
                    IdNhom = table.Column<int>(type: "int", nullable: false),
                    IdDoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinhDetails", x => x.IdDetails);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhDetails_ChuongTrinh_IdChuongTrinh",
                        column: x => x.IdChuongTrinh,
                        principalTable: "ChuongTrinh",
                        principalColumn: "IdChuongTrinh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhDetails_DiaDiem_IdDiaDiem",
                        column: x => x.IdDiaDiem,
                        principalTable: "DiaDiem",
                        principalColumn: "IdDiaDiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhDetails_DoanChuongTrinh_IdDoan",
                        column: x => x.IdDoan,
                        principalTable: "DoanChuongTrinh",
                        principalColumn: "IdDoan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhDetails_NhomChuongTrinh_IdNhom",
                        column: x => x.IdNhom,
                        principalTable: "NhomChuongTrinh",
                        principalColumn: "IdNhom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email_PhoneNumber",
                table: "Account",
                columns: new[] { "Email", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinh_Name",
                table: "ChuongTrinh",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhDetails_IdChuongTrinh",
                table: "ChuongTrinhDetails",
                column: "IdChuongTrinh");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhDetails_IdDiaDiem",
                table: "ChuongTrinhDetails",
                column: "IdDiaDiem");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhDetails_IdDoan",
                table: "ChuongTrinhDetails",
                column: "IdDoan");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhDetails_IdNhom",
                table: "ChuongTrinhDetails",
                column: "IdNhom");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhImage_IdChuongTrinh",
                table: "ChuongTrinhImage",
                column: "IdChuongTrinh");

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiem_IdAccount",
                table: "DiaDiem",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiem_IdSubMenu",
                table: "DiaDiem",
                column: "IdSubMenu");

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiem_Title",
                table: "DiaDiem",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiemMenu_Title",
                table: "DiaDiemMenu",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiemSubMenu_IdMenu",
                table: "DiaDiemSubMenu",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_DiaDiemSubMenu_Title",
                table: "DiaDiemSubMenu",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuHoTro_Title",
                table: "MenuHoTro",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhomChuongTrinh_Name",
                table: "NhomChuongTrinh",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_IdAccount",
                table: "TinTuc",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_TinTuc_Title",
                table: "TinTuc",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuongTrinhDetails");

            migrationBuilder.DropTable(
                name: "ChuongTrinhImage");

            migrationBuilder.DropTable(
                name: "MenuHoTro");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "DiaDiem");

            migrationBuilder.DropTable(
                name: "DoanChuongTrinh");

            migrationBuilder.DropTable(
                name: "NhomChuongTrinh");

            migrationBuilder.DropTable(
                name: "ChuongTrinh");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DiaDiemSubMenu");

            migrationBuilder.DropTable(
                name: "DiaDiemMenu");
        }
    }
}
