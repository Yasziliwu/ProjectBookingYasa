using BookingAPI.Models;

namespace BookingAPI.Contracts
{
    public interface IRuanganKaraokeRepository : IGeneralRepository<RuanganKaraoke>
    {
        Task<IEnumerable<RuanganKaraoke>> GetByCabangAsync(int cabangId);
    }
}
