using BookingClient.DTOs;

namespace BookingClient.Contract
{
    public interface IRuanganService
    {
        Task<RuanganDTO> GetRuanganByIdAsync(int id);
    }
}
