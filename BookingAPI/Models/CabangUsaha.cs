using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class CabangUsaha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cabang", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_cabang", TypeName = "nvarchar(50)")]
        public string NamaCabang { get; set; } = string.Empty;

        [Required]
        [Column("jenis_usaha", TypeName = "nvarchar(20)")]
        public string JenisUsaha { get; set; } = string.Empty; // ENUM('Karaoke', 'Pijat Refleksi')

        [Column("alamat_cabang", TypeName = "text")]
        public string? AlamatCabang { get; set; }

        [Column("nomor_telepon_cabang", TypeName = "nvarchar(20)")]
        public string? NomorTeleponCabang { get; set; }

        // Relasi ke ruangan karaoke dan pemesanan pijat
        public ICollection<RuanganKaraoke>? RuanganKaraokes { get; set; }
        public ICollection<PemesananPijat>? PemesananPijats { get; set; }
    }
}
