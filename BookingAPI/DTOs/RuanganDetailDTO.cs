namespace BookingAPI.DTOs
{
    public class RuanganDetailDto
    {
        public int Id { get; set; }
        public int CabangId { get; set; }
        public int NomorRuangan { get; set; }
        public int IdJenisRuangan { get; set; }

        public string NamaJenisRuangan { get; set; }
        public int KapasitasMaksimal { get; set; }
        public decimal HargaPerJam { get; set; }
    }
}
