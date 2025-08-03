namespace BookingAPI.DTOs
{
    public class TerapisDTO
    {
        public int Id { get; set; }
        public string NamaTerapis { get; set; } = null!;
        public string JenisKelamin { get; set; } = null!; // "Pria" / "Wanita"
        public string NomorTelepon { get; set; } = null!;
    }
}
