using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Repositories
{
    public class BookingKaraokeRepository : GeneralRepository<PemesananKaraoke>, IPemesananKaraokeRepository
    {
        private readonly AppDbContext _context;

        public BookingKaraokeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PemesananKaraoke>> GetAllAsync()
        {
            return await _context.PemesananKaraoke
                .Include(p => p.Pelanggan)
                .Include(p => p.RuanganKaraoke)
                .Include(p => p.PemanduKaraoke)
                .ToListAsync();
        }

        public async Task<PemesananKaraoke?> GetByIdAsync(int id)
        {
            return await _context.PemesananKaraoke
                .Include(p => p.Pelanggan)
                .Include(p => p.RuanganKaraoke)
                .Include(p => p.PemanduKaraoke)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
