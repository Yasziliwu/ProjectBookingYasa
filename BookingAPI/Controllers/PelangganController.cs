using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PelangganController : ControllerBase
    {
        private readonly IPelangganRepository _repository;
        private readonly AppDbContext _context;

        public PelangganController(IPelangganRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PelangganDTO dto)
        {
           var  pelanggan = new Pelanggan
            {
                Nama = dto.NamaPelanggan,
                Email = dto.Email,
                NomorTelepon = dto.NomorTelepon,
                Alamat = dto.Alamat
            };

            await _repository.AddAsync(pelanggan);
            var result = await _repository.SaveChangesAsync();
            if (!result) return BadRequest("Gagal menambahkan pelanggan");
            return Ok(pelanggan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pelanggan pelanggan)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Update manual (jika perlu field-field tertentu)
            existing.Nama = pelanggan.Nama;
            existing.Email = pelanggan.Email;
            existing.NomorTelepon = pelanggan.NomorTelepon;
            // dst...

            _repository.Update(existing);
            var result = await _repository.SaveChangesAsync();

            return result ? Ok(existing) : BadRequest("Update gagal");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _repository.Delete(existing);
            var result = await _repository.SaveChangesAsync();

            return result ? Ok("Berhasil dihapus") : BadRequest("Gagal hapus");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var pelanggan = await _repository.LoginAsync(login.Email);
            if (pelanggan == null)
                return Unauthorized("Email tidak ditemukan");

            var lastBooking = await _context.PemesananKaraoke
                .Where(p => p.IdPelanggan == pelanggan.Id && p.StatusPemesanan == "Pending")
                .OrderByDescending(p => p.TanggalPemesanan)
                .FirstOrDefaultAsync();

            // Mapping manual atau pakai AutoMapper
            var pelangganDto = new PelangganDTO
            {
                Id = pelanggan.Id,
                NamaPelanggan = pelanggan.Nama,
                Email = pelanggan.Email,
                NomorTelepon = pelanggan.NomorTelepon,
                Alamat = pelanggan.Alamat
            };

            var response = new
            {
                Pelanggan = pelangganDto,
                LastBookingId = lastBooking?.Id
            };

            return Ok(response);
        }

        // GET: api/pelanggan/email/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var pelanggan = await _repository.GetByEmailAsync(email);
            if (pelanggan == null)
                return NotFound("Pelanggan dengan email tersebut tidak ditemukan");

            return Ok(pelanggan);
        }
    }
}
