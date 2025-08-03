using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RuanganKaraokeController : ControllerBase
    {
        private readonly IGeneralRepository<RuanganKaraoke> _repo;

        public RuanganKaraokeController(IGeneralRepository<RuanganKaraoke> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ruangan = await _repo.GetAllAsync();
            var dto = ruangan.Select(x => new RuanganKaraokeDTO
            {
                Id = x.Id,
                CabangId = x.CabangId,
                NomorRuangan = x.NomorRuangan,
                IdJenisRuangan = x.JenisRuanganId
            });
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ruangan = await _repo.GetByIdAsync(id);
            if (ruangan == null) return NotFound();

            var dto = new RuanganKaraokeDTO
            {
                Id = ruangan.Id,
                CabangId = ruangan.CabangId,
                NomorRuangan = ruangan.NomorRuangan,
                IdJenisRuangan = ruangan.JenisRuanganId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RuanganKaraokeDTO dto)
        {
            var ruangan = new RuanganKaraoke
            {
                CabangId = dto.CabangId,
                NomorRuangan = dto.NomorRuangan,
                JenisRuanganId = dto.IdJenisRuangan
            };

            await _repo.AddAsync(ruangan);
            await _repo.SaveChangesAsync();

            dto.Id = ruangan.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RuanganKaraokeDTO dto)
        {
            var ruangan = await _repo.GetByIdAsync(id);
            if (ruangan == null) return NotFound();

            ruangan.CabangId = dto.CabangId;
            ruangan.NomorRuangan = dto.NomorRuangan;
            ruangan.JenisRuanganId = dto.IdJenisRuangan;

            _repo.Update(ruangan);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ruangan = await _repo.GetByIdAsync(id);
            if (ruangan == null) return NotFound();

            _repo.Delete(ruangan);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var ruangan = await _repo.Query()
                .Include(r => r.JenisRuanganKaraoke)
                .Where(r => r.Id == id)
                .Select(r => new RuanganDetailDTO
                {
                    Id = r.Id,
                    CabangId = r.CabangId,
                    NomorRuangan = r.NomorRuangan,
                    IdJenisRuangan = r.JenisRuanganId,
                    NamaJenisRuangan = r.JenisRuanganKaraoke.NamaJenisRuangan,
                    KapasitasMaksimal = r.JenisRuanganKaraoke.KapasitasMaksimal,
                    HargaPerJam = r.JenisRuanganKaraoke.HargaPerJam
                })
                .FirstOrDefaultAsync();

            if (ruangan == null) return NotFound();

            return Ok(ruangan);
        }
    }
}
