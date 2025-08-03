namespace BookingClient.DTOs
{
    public class BookingPijatSubmitDTO
    {
        public int PelangganId { get; set; }
        public int CabangUsahaId { get; set; }
        public int LayananPijatId { get; set; }
        public int? TerapisId { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public DateTime WaktuMulai { get; set; }
        public DateTime WaktuSelesai { get; set; }
    }
}
