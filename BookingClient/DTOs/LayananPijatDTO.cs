namespace BookingClient.DTOs
{
    public class LayananPijatDTO
    {
        public int Id { get; set; }
        public string NamaLayanan { get; set; } = string.Empty;
        public int DurasiMenit { get; set; }
        public decimal Harga { get; set; }
    }
}
