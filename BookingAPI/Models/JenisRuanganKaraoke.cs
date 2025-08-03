using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class JenisRuanganKaraoke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_jenis_ruangan", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_jenis_ruangan", TypeName = "nvarchar(20)")]
        public string NamaJenisRuangan { get; set; } = string.Empty;

        [Required]
        [Column("kapasitas_maksimal", TypeName = "int")]
        public int KapasitasMaksimal { get; set; }

        [Required]
        [Column("harga_per_jam", TypeName = "decimal(10,2)")]
        public decimal HargaPerJam { get; set; }

        // Relasi ke Ruangan Karaoke
        public ICollection<RuanganKaraoke>? RuanganKaraokes { get; set; }
    }
}
