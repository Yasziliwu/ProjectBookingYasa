using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class PemesananPijat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pemesanan_pijat", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("id_pelanggan", TypeName = "int")]
        public int PelangganId { get; set; }

        [Required]
        [Column("id_cabang", TypeName = "int")]
        public int CabangUsahaId { get; set; }

        [Required]
        [Column("id_layanan_pijat", TypeName = "int")]
        public int LayananPijatId { get; set; }

        [Column("id_terapis", TypeName = "int")]
        public int? TerapisId { get; set; }

        [Required]
        [Column("tanggal_pemesanan", TypeName = "date")]
        public DateTime TanggalPemesanan { get; set; }

        [Required]
        [Column("waktu_mulai", TypeName = "time")]
        public TimeSpan WaktuMulai { get; set; }

        [Required]
        [Column("waktu_selesai", TypeName = "time")]
        public TimeSpan WaktuSelesai { get; set; }

        [Required]
        [Column("total_harga", TypeName = "decimal(10,2)")]
        public decimal TotalHarga { get; set; }

        [Required]
        [Column("status_pemesanan", TypeName = "varchar(20)")]
        public string StatusPemesanan { get; set; } = "Pending";

        // Relasi Navigasi
        public Pelanggan? Pelanggan { get; set; }
        public CabangUsaha? CabangUsaha { get; set; }
        public LayananPijat? LayananPijat { get; set; }
        public Terapis? Terapis { get; set; }
    }
}
