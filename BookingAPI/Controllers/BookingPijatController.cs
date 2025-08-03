using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookingPijatController : ControllerBase
    {
        private readonly IGeneralRepository<PemesananPijat> _bookingRepo;
        private readonly IGeneralRepository<LayananPijat> _layananRepo;
        private readonly AppDbContext _context;

        public BookingPijatController(
            IGeneralRepository<PemesananPijat> bookingRepo,
            IGeneralRepository<LayananPijat> layananRepo, AppDbContext context)
        {
            _bookingRepo = bookingRepo;
            _layananRepo = layananRepo;
            _context = context;
        }       


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound("Booking tidak ditemukan.");

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingPijatDTO dto)
        {
            if (dto.WaktuSelesai <= dto.WaktuMulai)
                return BadRequest("Waktu selesai harus lebih besar dari waktu mulai.");

            var layanan = await _layananRepo.GetByIdAsync(dto.LayananPijatId);
            if (layanan == null)
                return NotFound("Layanan pijat tidak ditemukan.");

            var totalJam = (dto.WaktuSelesai - dto.WaktuMulai).TotalHours;
            var totalHarga = (decimal)totalJam * layanan.Harga;

            var booking = new PemesananPijat
            {
                PelangganId = dto.PelangganId,
                CabangUsahaId = dto.CabangUsahaId,
                LayananPijatId = dto.LayananPijatId,
                TerapisId = dto.TerapisId,
                TanggalPemesanan = dto.TanggalPemesanan.Date,
                WaktuMulai = dto.WaktuMulai.TimeOfDay,
                WaktuSelesai = dto.WaktuSelesai.TimeOfDay,
                TotalHarga = totalHarga,
                StatusPemesanan = "Pending"
            };

            await _bookingRepo.AddAsync(booking);
            var saved = await _bookingRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal menyimpan booking.");

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, new
            {
                Message = "Booking berhasil",
                BookingId = booking.Id,
                TotalHarga = totalHarga
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingPijatDTO dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound("Booking tidak ditemukan.");

            var layanan = await _layananRepo.GetByIdAsync(dto.LayananPijatId);
            if (layanan == null)
                return NotFound("Layanan pijat tidak ditemukan.");

            if (dto.WaktuSelesai <= dto.WaktuMulai)
                return BadRequest("Waktu selesai harus lebih besar dari waktu mulai.");

            var totalJam = (dto.WaktuSelesai - dto.WaktuMulai).TotalHours;
            var totalHarga = (decimal)totalJam * layanan.Harga;

            booking.PelangganId = dto.PelangganId;
            booking.CabangUsahaId = dto.CabangUsahaId;
            booking.LayananPijatId = dto.LayananPijatId;
            booking.TerapisId = dto.TerapisId;
            booking.TanggalPemesanan = dto.TanggalPemesanan.Date;
            booking.WaktuMulai = dto.WaktuMulai.TimeOfDay;
            booking.WaktuSelesai = dto.WaktuSelesai.TimeOfDay;
            booking.TotalHarga = totalHarga;

            _bookingRepo.Update(booking);
            var saved = await _bookingRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal memperbarui booking.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound("Booking tidak ditemukan.");

            _bookingRepo.Delete(booking);
            var saved = await _bookingRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal menghapus booking.");

            return NoContent();
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetBookingDetail(int id)
        {
            var booking = await _context.PemesananPijat
                .Include(p => p.Pelanggan)
                .Include(p => p.Terapis)
                .Include(p => p.LayananPijat)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (booking == null)
                return NotFound("Data booking tidak ditemukan.");

            var result = new BookingPijatDetailDTO
            {
                Id = booking.Id,
                NamaPelanggan = booking.Pelanggan?.Nama,
                NamaTerapis = booking.Terapis?.NamaTerapis ?? "(Belum ditentukan)",
                NamaLayanan = booking.LayananPijat?.NamaLayanan ?? "-",
                TanggalPemesanan = booking.TanggalPemesanan,
                WaktuMulai = booking.WaktuMulai,
                WaktuSelesai = booking.WaktuSelesai,
                TotalHarga = booking.TotalHarga,
                StatusPemesanan = booking.StatusPemesanan
            };

            return Ok(result);
        }
    }
}
