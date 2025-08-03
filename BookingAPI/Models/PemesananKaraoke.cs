using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class PemesananKaraoke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pemesanan_karaoke", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("id_pelanggan", TypeName = "int")]
        public int IdPelanggan { get; set; }

        [Required]
        [Column("id_ruangan", TypeName = "int")]
        public int RuanganKaraokeId { get; set; }

        [Column("id_pemandu", TypeName = "int")]
        public int? PemanduKaraokeId { get; set; }

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
        public RuanganKaraoke? RuanganKaraoke { get; set; }
        public PemanduKaraoke? PemanduKaraoke { get; set; }
    }
}
