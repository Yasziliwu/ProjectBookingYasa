namespace BookingClient.DTOs
{
    public class BookingPijatDTO
    {
        public int? Id { get; set; } // buat update, bisa nullable
        public int PelangganId { get; set; }
        public int CabangUsahaId { get; set; }
        public int LayananPijatId { get; set; }
        public int? TerapisId { get; set; }  // Nullable sesuai model
        public DateTime TanggalPemesanan { get; set; }
        public DateTime WaktuMulai { get; set; }
        public DateTime WaktuSelesai { get; set; }
        public decimal TotalHarga { get; set; }
        public string StatusPemesanan { get; set; } = "Pending";

        public LayananPijatDTO? LayananPijat { get; set; }  // Tambahkan ini
    }
}
