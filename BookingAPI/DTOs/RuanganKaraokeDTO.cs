namespace BookingAPI.DTOs
{
    public class RuanganKaraokeDTO
    {
        public int Id { get; set; }
        public int CabangId { get; set; }
        public string NomorRuangan { get; set; } = null!;
        public int IdJenisRuangan { get; set; }
    }
}
