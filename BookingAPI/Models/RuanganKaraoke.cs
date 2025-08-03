using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class RuanganKaraoke
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_ruangan", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("id_cabang", TypeName = "int")]
        public int CabangId { get; set; }

        [Required]
        [Column("nomor_ruangan", TypeName = "nvarchar(10)")]
        public string NomorRuangan { get; set; } = string.Empty;

        [Required]
        [Column("id_jenis_ruangan", TypeName = "int")]
        public int JenisRuanganId { get; set; }

        // Navigasi
        public CabangUsaha? CabangUsaha { get; set; }
        public JenisRuanganKaraoke? JenisRuanganKaraoke { get; set; }

        public ICollection<PemesananKaraoke>? PemesananKaraokes { get; set; }
    }
}
