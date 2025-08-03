using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingAPI.Models
{
    public class Terapis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_terapis", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_terapis", TypeName = "nvarchar(50)")]
        public string NamaTerapis { get; set; } = string.Empty;

        [Required]
        [Column("jenis_kelamin", TypeName = "varchar(10)")]
        public string JenisKelamin { get; set; } = string.Empty;

        [Column("nomor_telepon", TypeName = "varchar(30)")]
        public string? NomorTelepon { get; set; }

        // Relasi
        public ICollection<PemesananPijat>? PemesananPijats { get; set; }
    }
}
