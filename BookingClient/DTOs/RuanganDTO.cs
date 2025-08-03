namespace BookingClient.DTOs
{
    public class RuanganDTO
    {
        public int Id { get; set; }                 // ID ruangan
        public string Nama { get; set; }            // Nama ruangan (misalnya: "VIP", "Deluxe")
        public decimal HargaPerJam { get; set; }
    }
}
