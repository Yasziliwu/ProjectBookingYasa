namespace BookingAPI.DTOs
{
    public class LayananPijatDTO
    {
        public int Id { get; set; }
        public string NamaLayanan { get; set; } = null!;
        public int DurasiMenit { get; set; }
        public decimal Harga { get; set; }
    }
}
