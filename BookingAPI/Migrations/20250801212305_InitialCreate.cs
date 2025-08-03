using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CabangUsaha",
                columns: table => new
                {
                    id_cabang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_cabang = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    jenis_usaha = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    alamat_cabang = table.Column<string>(type: "text", nullable: true),
                    nomor_telepon_cabang = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabangUsaha", x => x.id_cabang);
                });

            migrationBuilder.CreateTable(
                name: "JenisRuanganKaraoke",
                columns: table => new
                {
                    id_jenis_ruangan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_jenis_ruangan = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    kapasitas_maksimal = table.Column<int>(type: "int", nullable: false),
                    harga_per_jam = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisRuanganKaraoke", x => x.id_jenis_ruangan);
                });

            migrationBuilder.CreateTable(
                name: "LayananPijat",
                columns: table => new
                {
                    id_layanan_pijat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_layanan = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    durasi_menit = table.Column<int>(type: "int", nullable: false),
                    harga = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayananPijat", x => x.id_layanan_pijat);
                });

            migrationBuilder.CreateTable(
                name: "Pelanggan",
                columns: table => new
                {
                    id_pelanggan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_pelanggan = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    nomor_telepon = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    alamat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelanggan", x => x.id_pelanggan);
                });

            migrationBuilder.CreateTable(
                name: "PemanduKaraoke",
                columns: table => new
                {
                    id_pemandu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_pemandu = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    jenis_kelamin = table.Column<string>(type: "varchar(10)", nullable: false),
                    nomor_telepon = table.Column<string>(type: "varchar(15)", nullable: true),
                    status_ketersediaan = table.Column<string>(type: "varchar(20)", nullable: false),
                    deskripsi = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PemanduKaraoke", x => x.id_pemandu);
                });

            migrationBuilder.CreateTable(
                name: "Terapis",
                columns: table => new
                {
                    id_terapis = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nama_terapis = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    jenis_kelamin = table.Column<string>(type: "varchar(10)", nullable: false),
                    nomor_telepon = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terapis", x => x.id_terapis);
                });

            migrationBuilder.CreateTable(
                name: "RuanganKaraoke",
                columns: table => new
                {
                    id_ruangan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cabang = table.Column<int>(type: "int", nullable: false),
                    nomor_ruangan = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    id_jenis_ruangan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuanganKaraoke", x => x.id_ruangan);
                    table.ForeignKey(
                        name: "FK_RuanganKaraoke_CabangUsaha_id_cabang",
                        column: x => x.id_cabang,
                        principalTable: "CabangUsaha",
                        principalColumn: "id_cabang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuanganKaraoke_JenisRuanganKaraoke_id_jenis_ruangan",
                        column: x => x.id_jenis_ruangan,
                        principalTable: "JenisRuanganKaraoke",
                        principalColumn: "id_jenis_ruangan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PemesananPijat",
                columns: table => new
                {
                    id_pemesanan_pijat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pelanggan = table.Column<int>(type: "int", nullable: false),
                    id_cabang = table.Column<int>(type: "int", nullable: false),
                    id_layanan_pijat = table.Column<int>(type: "int", nullable: false),
                    id_terapis = table.Column<int>(type: "int", nullable: true),
                    tanggal_pemesanan = table.Column<DateTime>(type: "date", nullable: false),
                    waktu_mulai = table.Column<TimeSpan>(type: "time", nullable: false),
                    waktu_selesai = table.Column<TimeSpan>(type: "time", nullable: false),
                    total_harga = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status_pemesanan = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PemesananPijat", x => x.id_pemesanan_pijat);
                    table.ForeignKey(
                        name: "FK_PemesananPijat_CabangUsaha_id_cabang",
                        column: x => x.id_cabang,
                        principalTable: "CabangUsaha",
                        principalColumn: "id_cabang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PemesananPijat_LayananPijat_id_layanan_pijat",
                        column: x => x.id_layanan_pijat,
                        principalTable: "LayananPijat",
                        principalColumn: "id_layanan_pijat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PemesananPijat_Pelanggan_id_pelanggan",
                        column: x => x.id_pelanggan,
                        principalTable: "Pelanggan",
                        principalColumn: "id_pelanggan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PemesananPijat_Terapis_id_terapis",
                        column: x => x.id_terapis,
                        principalTable: "Terapis",
                        principalColumn: "id_terapis");
                });

            migrationBuilder.CreateTable(
                name: "PemesananKaraoke",
                columns: table => new
                {
                    id_pemesanan_karaoke = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pelanggan = table.Column<int>(type: "int", nullable: false),
                    id_ruangan = table.Column<int>(type: "int", nullable: false),
                    id_pemandu = table.Column<int>(type: "int", nullable: true),
                    tanggal_pemesanan = table.Column<DateTime>(type: "date", nullable: false),
                    waktu_mulai = table.Column<TimeSpan>(type: "time", nullable: false),
                    waktu_selesai = table.Column<TimeSpan>(type: "time", nullable: false),
                    total_harga = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status_pemesanan = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PemesananKaraoke", x => x.id_pemesanan_karaoke);
                    table.ForeignKey(
                        name: "FK_PemesananKaraoke_Pelanggan_id_pelanggan",
                        column: x => x.id_pelanggan,
                        principalTable: "Pelanggan",
                        principalColumn: "id_pelanggan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PemesananKaraoke_PemanduKaraoke_id_pemandu",
                        column: x => x.id_pemandu,
                        principalTable: "PemanduKaraoke",
                        principalColumn: "id_pemandu");
                    table.ForeignKey(
                        name: "FK_PemesananKaraoke_RuanganKaraoke_id_ruangan",
                        column: x => x.id_ruangan,
                        principalTable: "RuanganKaraoke",
                        principalColumn: "id_ruangan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PemesananKaraoke_id_pelanggan",
                table: "PemesananKaraoke",
                column: "id_pelanggan");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananKaraoke_id_pemandu",
                table: "PemesananKaraoke",
                column: "id_pemandu");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananKaraoke_id_ruangan",
                table: "PemesananKaraoke",
                column: "id_ruangan");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananPijat_id_cabang",
                table: "PemesananPijat",
                column: "id_cabang");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananPijat_id_layanan_pijat",
                table: "PemesananPijat",
                column: "id_layanan_pijat");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananPijat_id_pelanggan",
                table: "PemesananPijat",
                column: "id_pelanggan");

            migrationBuilder.CreateIndex(
                name: "IX_PemesananPijat_id_terapis",
                table: "PemesananPijat",
                column: "id_terapis");

            migrationBuilder.CreateIndex(
                name: "IX_RuanganKaraoke_id_cabang",
                table: "RuanganKaraoke",
                column: "id_cabang");

            migrationBuilder.CreateIndex(
                name: "IX_RuanganKaraoke_id_jenis_ruangan",
                table: "RuanganKaraoke",
                column: "id_jenis_ruangan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PemesananKaraoke");

            migrationBuilder.DropTable(
                name: "PemesananPijat");

            migrationBuilder.DropTable(
                name: "PemanduKaraoke");

            migrationBuilder.DropTable(
                name: "RuanganKaraoke");

            migrationBuilder.DropTable(
                name: "LayananPijat");

            migrationBuilder.DropTable(
                name: "Pelanggan");

            migrationBuilder.DropTable(
                name: "Terapis");

            migrationBuilder.DropTable(
                name: "CabangUsaha");

            migrationBuilder.DropTable(
                name: "JenisRuanganKaraoke");
        }
    }
}
