using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PemanduKaraokeController : ControllerBase
    {
        private readonly IGeneralRepository<PemanduKaraoke> _pemanduRepo;

        public PemanduKaraokeController(IGeneralRepository<PemanduKaraoke> pemanduRepo)
        {
            _pemanduRepo = pemanduRepo;
        }

        // GET: api/PemanduKaraoke
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _pemanduRepo.GetAllAsync();
            var result = data.Select(p => new PemanduKaraokeDTO
            {
                Id = p.Id,
                NamaPemandu = p.NamaPemandu,
                JenisKelamin = p.JenisKelamin,
                NomorTelepon = p.NomorTelepon ?? "",
                StatusKetersediaan = p.StatusKetersediaan,
                Deskripsi = p.Deskripsi
            });

            return Ok(result);
        }

        // GET: api/PemanduKaraoke/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pemandu = await _pemanduRepo.GetByIdAsync(id);
            if (pemandu == null) return NotFound();

            var dto = new PemanduKaraokeDTO
            {
                Id = pemandu.Id,
                NamaPemandu = pemandu.NamaPemandu,
                JenisKelamin = pemandu.JenisKelamin,
                NomorTelepon = pemandu.NomorTelepon ?? "",
                StatusKetersediaan = pemandu.StatusKetersediaan,
                Deskripsi = pemandu.Deskripsi
            };

            return Ok(dto);
        }

        // POST: api/PemanduKaraoke
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PemanduKaraokeDTO dto)
        {
            var entity = new PemanduKaraoke
            {
                NamaPemandu = dto.NamaPemandu,
                JenisKelamin = dto.JenisKelamin,
                NomorTelepon = dto.NomorTelepon,
                StatusKetersediaan = dto.StatusKetersediaan ?? "Tersedia",
                Deskripsi = dto.Deskripsi
            };

            await _pemanduRepo.AddAsync(entity);
            var success = await _pemanduRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal menambahkan pemandu.");
            dto.Id = entity.Id; // Ambil ID hasil insert

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        }

        // PUT: api/PemanduKaraoke/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PemanduKaraokeDTO dto)
        {
            var entity = await _pemanduRepo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            entity.NamaPemandu = dto.NamaPemandu;
            entity.JenisKelamin = dto.JenisKelamin;
            entity.NomorTelepon = dto.NomorTelepon;
            entity.StatusKetersediaan = dto.StatusKetersediaan;
            entity.Deskripsi = dto.Deskripsi;

            _pemanduRepo.Update(entity);
            var success = await _pemanduRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal memperbarui data pemandu.");

            return NoContent();
        }

        // DELETE: api/PemanduKaraoke/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _pemanduRepo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            _pemanduRepo.Delete(entity);
            var success = await _pemanduRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal menghapus pemandu.");

            return NoContent();
        }
    }
}
