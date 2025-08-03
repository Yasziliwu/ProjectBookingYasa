namespace BookingAPI.DTOs
{
    public class BookingKaraokeDTO
    {
        public int IdPelanggan { get; set; }
        public int RuanganId { get; set; }
        public int? PemanduId { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public DateTime WaktuMulai { get; set; }
        public DateTime WaktuSelesai { get; set; }
        public decimal TotalHarga { get; set; }
    }
}
