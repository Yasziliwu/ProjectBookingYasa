namespace BookingClient.DTOs
{
    public class TerapisDTO
    {
        public int Id { get; set; }
        public string NamaTerapis { get; set; } = string.Empty;
        public string JenisKelamin { get; set; } = string.Empty;
        public string? NomorTelepon { get; set; }
    }
}
