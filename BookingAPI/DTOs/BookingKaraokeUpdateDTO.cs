using System.ComponentModel.DataAnnotations;

namespace BookingAPI.DTOs
{
    public class BookingKaraokeUpdateDTO
    {
        public class BookingKaraokeUpdateDto
        {
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

            [Required]
            public string StatusPemesanan { get; set; } = "Pending";
        }
    }
}
