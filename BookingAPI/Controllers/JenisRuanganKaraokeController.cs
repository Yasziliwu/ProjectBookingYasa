using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JenisRuanganKaraokeController : ControllerBase
    {
        private readonly IGeneralRepository<JenisRuanganKaraoke> _repo;

        public JenisRuanganKaraokeController(IGeneralRepository<JenisRuanganKaraoke> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jenis = await _repo.GetAllAsync();
            var dto = jenis.Select(x => new JenisRuanganKaraokeDTO
            {
                Id = x.Id,
                NamaJenisRuangan = x.NamaJenisRuangan,
                KapasitasMaksimal = x.KapasitasMaksimal,
                HargaPerJam = x.HargaPerJam
            });
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JenisRuanganKaraokeDTO dto)
        {
            var jenis = new JenisRuanganKaraoke
            {
                NamaJenisRuangan = dto.NamaJenisRuangan,
                KapasitasMaksimal = dto.KapasitasMaksimal,
                HargaPerJam = dto.HargaPerJam
            };

            await _repo.AddAsync(jenis);
            await _repo.SaveChangesAsync();

            dto.Id = jenis.Id;
            return CreatedAtAction(nameof(GetAll), new { id = dto.Id }, dto);
        }
    }
}
