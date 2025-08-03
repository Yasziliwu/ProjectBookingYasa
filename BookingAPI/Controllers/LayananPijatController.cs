using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LayananPijatController : ControllerBase
    {
        private readonly IGeneralRepository<LayananPijat> _layananRepo;

        public LayananPijatController(IGeneralRepository<LayananPijat> layananRepo)
        {
            _layananRepo = layananRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LayananPijatDTO dto)
        {
            var layanan = new LayananPijat
            {
                NamaLayanan = dto.NamaLayanan,
                DurasiMenit = dto.DurasiMenit,
                Harga = dto.Harga
            };

            await _layananRepo.AddAsync(layanan);
            var saved = await _layananRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal menambahkan layanan.");

            // Return DTO instead of entity
            var resultDto = new LayananPijatDTO
            {
                Id = layanan.Id,
                NamaLayanan = layanan.NamaLayanan,
                DurasiMenit = layanan.DurasiMenit,
                Harga = layanan.Harga
            };

            return CreatedAtAction(nameof(GetById), new { id = layanan.Id }, resultDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var layananList = await _layananRepo.GetAllAsync();

            var dtoList = layananList.Select(l => new LayananPijatDTO
            {
                Id = l.Id,
                NamaLayanan = l.NamaLayanan,
                DurasiMenit = l.DurasiMenit,
                Harga = l.Harga
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var layanan = await _layananRepo.GetByIdAsync(id);
            if (layanan == null)
                return NotFound();

            var dto = new LayananPijatDTO
            {
                Id = layanan.Id,
                NamaLayanan = layanan.NamaLayanan,
                DurasiMenit = layanan.DurasiMenit,
                Harga = layanan.Harga
            };

            return Ok(dto);
        }
    }
}
