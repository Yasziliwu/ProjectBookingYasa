using BookingAPI.Data;
using BookingAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PilihRuanganKaraokeController : ControllerBase
    {

        private readonly AppDbContext _context;

        public PilihRuanganKaraokeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var result = await _context.RuanganKaraoke
                .Include(r => r.CabangUsaha)
                .Include(r => r.JenisRuanganKaraoke)
                .Select(r => new PilihRuanganKaraokeDTO
                {
                    IdRuangan = r.Id,
                    NomorRuangan = r.NomorRuangan,
                    NamaCabang = r.CabangUsaha.NamaCabang,
                    JenisUsaha = r.CabangUsaha.JenisUsaha,
                    NamaJenisRuangan = r.JenisRuanganKaraoke.NamaJenisRuangan,
                    KapasitasMaksimal = r.JenisRuanganKaraoke.KapasitasMaksimal,
                    HargaPerJam = r.JenisRuanganKaraoke.HargaPerJam
                })
                .ToListAsync();

            return Ok(result);
        }
    }
}
