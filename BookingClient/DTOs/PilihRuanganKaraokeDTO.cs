namespace BookingClient.DTOs
{
    public class PilihRuanganKaraokeDTO
    {
        public int IdRuangan { get; set; }
        public string NomorRuangan { get; set; }
        public string NamaCabang { get; set; }
        public string JenisUsaha { get; set; }
        public string NamaJenisRuangan { get; set; }
        public int KapasitasMaksimal { get; set; }
        public decimal HargaPerJam { get; set; }
    }
}
