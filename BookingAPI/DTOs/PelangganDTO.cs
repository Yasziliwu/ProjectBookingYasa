namespace BookingAPI.DTOs
{
    public class PelangganDTO
    {
        public int Id { get; set; }
        public string NamaPelanggan { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NomorTelepon { get; set; } = null!;
        public string Alamat { get; set; } = null!;
    }
}
