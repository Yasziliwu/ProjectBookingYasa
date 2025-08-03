namespace BookingAPI.Controllers
{
    internal class RuanganDetailDTO
    {
        public object Id { get; set; }
        public object CabangId { get; set; }
        public object NomorRuangan { get; set; }
        public object IdJenisRuangan { get; set; }
        public object NamaJenisRuangan { get; set; }
        public object KapasitasMaksimal { get; set; }
        public object HargaPerJam { get; set; }
    }
}