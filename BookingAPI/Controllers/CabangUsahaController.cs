using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CabangUsahaController : ControllerBase
    {
        private readonly IGeneralRepository<CabangUsaha> _repo;

        public CabangUsahaController(IGeneralRepository<CabangUsaha> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repo.GetAllAsync();
            var dto = list.Select(x => new CabangUsahaDTO
            {
                Id = x.Id,
                NamaCabang = x.NamaCabang,
                JenisUsaha = x.JenisUsaha,
                AlamatCabang = x.AlamatCabang ?? "",
                NomorTeleponCabang = x.NomorTeleponCabang ?? ""
            });
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cabang = await _repo.GetByIdAsync(id);
            if (cabang == null) return NotFound();

            var dto = new CabangUsahaDTO
            {
                Id = cabang.Id,
                NamaCabang = cabang.NamaCabang,
                JenisUsaha = cabang.JenisUsaha,
                AlamatCabang = cabang.AlamatCabang ?? "",
                NomorTeleponCabang = cabang.NomorTeleponCabang ?? ""
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CabangUsahaDTO dto)
        {
            var cabang = new CabangUsaha
            {
                NamaCabang = dto.NamaCabang,
                JenisUsaha = dto.JenisUsaha,
                AlamatCabang = dto.AlamatCabang,
                NomorTeleponCabang = dto.NomorTeleponCabang
            };

            await _repo.AddAsync(cabang);
            await _repo.SaveChangesAsync();

            dto.Id = cabang.Id;
            return CreatedAtAction(nameof(GetById), new { id = cabang.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CabangUsahaDTO dto)
        {
            var cabang = await _repo.GetByIdAsync(id);
            if (cabang == null) return NotFound();

            cabang.NamaCabang = dto.NamaCabang;
            cabang.JenisUsaha = dto.JenisUsaha;
            cabang.AlamatCabang = dto.AlamatCabang;
            cabang.NomorTeleponCabang = dto.NomorTeleponCabang;

            _repo.Update(cabang);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cabang = await _repo.GetByIdAsync(id);
            if (cabang == null) return NotFound();

            _repo.Delete(cabang);
            await _repo.SaveChangesAsync();

            return NoContent();
        }
    }
}
