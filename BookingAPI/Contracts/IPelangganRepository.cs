using BookingAPI.Models;

namespace BookingAPI.Contracts
{
    public interface IPelangganRepository : IGeneralRepository<Pelanggan>
    {
        Task<Pelanggan?> GetByEmailAsync(string email);
        Task<Pelanggan?> LoginAsync(string email);
        //IQueryable<Pelanggan> Query(); // Tambahkan ini
    }
}
