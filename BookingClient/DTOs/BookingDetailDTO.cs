namespace BookingClient.DTOs
{
    public class BookingDetailDTO
    {
        public int Id { get; set; }
        public string? NamaPelanggan { get; set; }
        public string? NamaPemandu { get; set; }
        public string? NomorRuangan { get; set; }
        public string? JenisRuangan { get; set; }
        public DateTime TanggalPemesanan { get; set; }
        public TimeSpan WaktuMulai { get; set; }
        public TimeSpan WaktuSelesai { get; set; }
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; }
    }
}
