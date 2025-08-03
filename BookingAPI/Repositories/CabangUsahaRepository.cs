using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Repository;

namespace BookingAPI.Repositories
{
    public class CabangUsahaRepository : GeneralRepository<CabangUsaha>, ICabangUsahaRepository
    {
        private readonly AppDbContext _context;

        public CabangUsahaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }       
    }
}
