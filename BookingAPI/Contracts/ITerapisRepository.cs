using BookingAPI.Models;

namespace BookingAPI.Contracts
{
    public interface ITerapisRepository : IGeneralRepository<Terapis>
    {
        Task<IEnumerable<Terapis>> GetAvailableAsync(DateTime tanggal);
    }
}
