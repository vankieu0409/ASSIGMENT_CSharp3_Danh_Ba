using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASSIGMENT_Danh_Ba.Migrations
{
    public partial class Nguoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nguoi",
                columns: table => new
                {
                    IdNguoi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ho = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TenDem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NamSinh = table.Column<int>(type: "int", nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nguoi", x => x.IdNguoi);
                });

            migrationBuilder.CreateTable(
                name: "DANHBA",
                columns: table => new
                {
                    IdDanhBa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SDT1 = table.Column<int>(type: "int", nullable: true),
                    SDT2 = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IdNguoi = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANHBA", x => x.IdDanhBa);
                    table.ForeignKey(
                        name: "FK_DANHBA_Nguoi_IdNguoi",
                        column: x => x.IdNguoi,
                        principalTable: "Nguoi",
                        principalColumn: "IdNguoi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DANHBA_IdNguoi",
                table: "DANHBA",
                column: "IdNguoi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DANHBA");

            migrationBuilder.DropTable(
                name: "Nguoi");
        }
    }
}
