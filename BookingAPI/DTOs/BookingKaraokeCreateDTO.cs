using System.ComponentModel.DataAnnotations;

namespace BookingAPI.DTOs
{
    public class BookingKaraokeCreateDTO
    {
        public class BookingKaraokeCreateDto
        {
            [Required]
            public int IdPelanggan { get; set; }

            [Required]
            public int RuanganKaraokeId { get; set; }

            public int? PemanduKaraokeId { get; set; }

            [Required]
            public DateTime TanggalPemesanan { get; set; }

            [Required]
            public TimeSpan WaktuMulai { get; set; }

            [Required]
            public TimeSpan WaktuSelesai { get; set; }

            [Required]
            public decimal TotalHarga { get; set; }
        }
    }
}
