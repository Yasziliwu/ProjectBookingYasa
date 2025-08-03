namespace BookingAPI.DTOs
{
    public class JenisRuanganKaraokeDTO
    {
        public int Id { get; set; }
        public string NamaJenisRuangan { get; set; } = null!;
        public int KapasitasMaksimal { get; set; }
        public decimal HargaPerJam { get; set; }
    }
}
