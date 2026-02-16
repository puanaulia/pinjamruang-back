using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PeminjamRuangAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peminjaman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaPeminjam = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ruangan = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TanggalPinjam = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TanggalSelesai = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Tujuan = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Menunggu"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjaman", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Peminjaman",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "NamaPeminjam", "Ruangan", "Status", "TanggalPinjam", "TanggalSelesai", "Tujuan", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 14, 16, 31, 16, 483, DateTimeKind.Local).AddTicks(4319), false, "Budi Santoso", "Ruang 101", "Disetujui", new DateTime(2026, 2, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null, "Rapat Tim", null },
                    { 2, new DateTime(2026, 2, 14, 16, 31, 16, 483, DateTimeKind.Local).AddTicks(4338), false, "Siti Aminah", "Lab Komputer", "Menunggu", new DateTime(2026, 2, 16, 13, 0, 0, 0, DateTimeKind.Unspecified), null, "Praktikum", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjaman");
        }
    }
}
