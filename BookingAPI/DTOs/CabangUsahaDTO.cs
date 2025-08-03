
namespace BookingAPI.DTOs
{
    public class CabangUsahaDTO
    {
        public int Id { get; set; }
        public string NamaCabang { get; set; } = null!;
        public string JenisUsaha { get; set; } = null!; // "Karaoke" / "Pijat Refleksi"
        public string AlamatCabang { get; set; } = null!;
        public string NomorTeleponCabang { get; set; } = null!;
    }
}
