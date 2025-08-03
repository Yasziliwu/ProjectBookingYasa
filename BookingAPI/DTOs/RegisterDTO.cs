namespace BookingAPI.DTOs
{
    public class RegisterDTO
    {
        public string NamaPelanggan { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NomorTelepon { get; set; } = null!;
        public string Alamat { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
