namespace BookingAPI.DTOs
{
    public class BookingPijatDetailDTO
    {
        public int Id { get; set; }
        public string? NamaPelanggan { get; set; }
        public string? NamaTerapis { get; set; }
        public string? NamaLayanan { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public TimeSpan WaktuMulai { get; set; }
        public TimeSpan WaktuSelesai { get; set; }
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; }
    }
}
