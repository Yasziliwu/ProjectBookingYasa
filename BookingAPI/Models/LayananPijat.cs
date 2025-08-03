using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class LayananPijat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_layanan_pijat", TypeName = "int")]
        public int Id { get; set; }

        [Required]
        [Column("nama_layanan", TypeName = "nvarchar(50)")]
        public string NamaLayanan { get; set; } = string.Empty;

        [Required]
        [Column("durasi_menit", TypeName = "int")]
        public int DurasiMenit { get; set; }

        [Required]
        [Column("harga", TypeName = "decimal(10,2)")]
        public decimal Harga { get; set; }

        // Relasi
        public ICollection<PemesananPijat>? PemesananPijats { get; set; }
    }
}
