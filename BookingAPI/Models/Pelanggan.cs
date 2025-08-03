using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class Pelanggan
    {
        [Key]
        [Column("id_pelanggan", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_pelanggan", TypeName = "nvarchar(25)")]
        public string Nama { get; set; } = string.Empty;

        [Required]
        [Column("email", TypeName = "nvarchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Column("nomor_telepon", TypeName = "nvarchar(25)")]
        public string? NomorTelepon { get; set; }

        [Column("alamat", TypeName = "nvarchar(max)")]
        public string? Alamat { get; set; }

        // Relasi ke pemesanan
        public ICollection<PemesananKaraoke>? PemesananKaraokes { get; set; }
        public ICollection<PemesananPijat>? PemesananPijats { get; set; }
    }
}
