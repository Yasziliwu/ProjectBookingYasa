using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerapisController : ControllerBase
    {
        private readonly IGeneralRepository<Terapis> _terapisRepo;

        public TerapisController(IGeneralRepository<Terapis> terapisRepo)
        {
            _terapisRepo = terapisRepo;
        }

        // GET: api/Terapis
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _terapisRepo.GetAllAsync();
            var result = data.Select(t => new TerapisDTO
            {
                Id = t.Id,
                NamaTerapis = t.NamaTerapis,
                JenisKelamin = t.JenisKelamin,
                NomorTelepon = t.NomorTelepon ?? ""
            });

            return Ok(result);
        }

        // GET: api/Terapis/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var terapis = await _terapisRepo.GetByIdAsync(id);
            if (terapis == null) return NotFound();

            var dto = new TerapisDTO
            {
                Id = terapis.Id,
                NamaTerapis = terapis.NamaTerapis,
                JenisKelamin = terapis.JenisKelamin,
                NomorTelepon = terapis.NomorTelepon ?? ""
            };

            return Ok(dto);
        }

        // POST: api/Terapis
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TerapisDTO dto)
        {
            var entity = new Terapis
            {
                NamaTerapis = dto.NamaTerapis,
                JenisKelamin = dto.JenisKelamin,
                NomorTelepon = dto.NomorTelepon
            };

            await _terapisRepo.AddAsync(entity);
            var success = await _terapisRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal menambahkan terapis.");

            dto.Id = entity.Id;
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        }

        // PUT: api/Terapis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TerapisDTO dto)
        {
            var entity = await _terapisRepo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            entity.NamaTerapis = dto.NamaTerapis;
            entity.JenisKelamin = dto.JenisKelamin;
            entity.NomorTelepon = dto.NomorTelepon;

            _terapisRepo.Update(entity);
            var success = await _terapisRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal mengupdate terapis.");

            return NoContent();
        }

        // DELETE: api/Terapis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _terapisRepo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            _terapisRepo.Delete(entity);
            var success = await _terapisRepo.SaveChangesAsync();

            if (!success) return BadRequest("Gagal menghapus terapis.");

            return NoContent();
        }
    }
}
