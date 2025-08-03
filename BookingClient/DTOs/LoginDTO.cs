namespace BookingClient.DTOs
{
    public class LoginDTO
    {
        public PelangganDTO Pelanggan { get; set; } = null!;
        public int? LastBookingId { get; set; }
        public string Email { get; set; } = string.Empty;


    }

}
