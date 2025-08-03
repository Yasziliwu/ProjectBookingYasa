using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class PemanduKaraoke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_pemandu", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_pemandu", TypeName = "nvarchar(50)")]
        public string NamaPemandu { get; set; } = string.Empty;

        [Required]
        [Column("jenis_kelamin", TypeName = "varchar(10)")]
        public string JenisKelamin { get; set; } = string.Empty;

        [Column("nomor_telepon", TypeName = "varchar(15)")]
        public string? NomorTelepon { get; set; }

        [Required]
        [Column("status_ketersediaan", TypeName = "varchar(20)")]
        public string StatusKetersediaan { get; set; } = "Tersedia";

        [Column("deskripsi", TypeName = "text")]
        public string? Deskripsi { get; set; }

        // Navigasi
        public ICollection<PemesananKaraoke>? PemesananKaraokes { get; set; }
    }
}
