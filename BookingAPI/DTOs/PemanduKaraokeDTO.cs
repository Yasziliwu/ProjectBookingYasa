namespace BookingAPI.DTOs
{
        public class PemanduKaraokeDTO
        {
            public int Id { get; set; }
            public string NamaPemandu { get; set; } = null!;
            public string JenisKelamin { get; set; } = null!;
            public string NomorTelepon { get; set; } = null!;
            public string StatusKetersediaan { get; set; } = "Tersedia";
            public string? Deskripsi { get; set; }
        }
}
