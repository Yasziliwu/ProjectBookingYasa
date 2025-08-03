using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Repositories
{
    public class PelangganRepository : GeneralRepository<Pelanggan>, IPelangganRepository
    {
        private readonly AppDbContext _context;

        public PelangganRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pelanggan?> LoginAsync(string email)
        {
            return await _context.Pelanggan
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Pelanggan?> GetByEmailAsync(string email)
        {
            return await _context.Pelanggan.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
