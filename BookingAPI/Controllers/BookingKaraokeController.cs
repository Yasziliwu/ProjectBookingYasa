using BookingAPI.Contracts;
using BookingAPI.Data;
using BookingAPI.DTOs;
using BookingAPI.Models;
using BookingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookingAPI.DTOs.BookingKaraokeCreateDTO;
using static BookingAPI.DTOs.BookingKaraokeUpdateDTO;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingKaraokeController : ControllerBase
    {
        private readonly IGeneralRepository<PemesananKaraoke> _bookingRepo;
        private readonly AppDbContext _context;
        public BookingKaraokeController(IGeneralRepository<PemesananKaraoke> bookingRepo, AppDbContext context)
        {
            _bookingRepo = bookingRepo;
            _context = context;

        }

        // GET: api/BookingKaraoke
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return Ok(bookings);
        }

        // GET: api/BookingKaraoke/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // POST: api/BookingKaraoke
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingKaraokeDTO dto)
        {

            if (dto.WaktuMulai >= dto.WaktuSelesai)
            {
                return BadRequest("Waktu mulai harus lebih awal dari waktu selesai.");
            }
            // Ambil semua booking pada tanggal dan ruangan yang sama
            var existingBookings = await _bookingRepo.GetAllAsync();

            var isConflict = existingBookings.Any(b =>
            b.RuanganKaraokeId == dto.RuanganId &&
            b.TanggalPemesanan.Date == dto.TanggalPemesanan.Date &&
            b.WaktuMulai < dto.WaktuSelesai.TimeOfDay &&
            b.WaktuSelesai > dto.WaktuMulai.TimeOfDay
          );

            if (isConflict)
            {
                return Conflict("Ruangan sudah dibooking pada waktu tersebut.");
            }

            var booking = new PemesananKaraoke
            {
                IdPelanggan = dto.IdPelanggan,
                RuanganKaraokeId = dto.RuanganId,
                PemanduKaraokeId = dto.PemanduId,
                TanggalPemesanan = dto.TanggalPemesanan,
                WaktuMulai = dto.WaktuMulai.TimeOfDay,
                WaktuSelesai = dto.WaktuSelesai.TimeOfDay,
                TotalHarga = dto.TotalHarga,
                StatusPemesanan = "Pending"
            };

            await _bookingRepo.AddAsync(booking);
            var saved = await _bookingRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal menyimpan data booking.");

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        // PUT: api/BookingKaraoke/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookingKaraokeDTO dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound();

            booking.RuanganKaraokeId = dto.RuanganId;
            booking.PemanduKaraokeId = dto.PemanduId;
            booking.TanggalPemesanan = dto.TanggalPemesanan;
            booking.WaktuMulai = dto.WaktuMulai.TimeOfDay;
            booking.WaktuSelesai = dto.WaktuSelesai.TimeOfDay;

            _bookingRepo.Update(booking);
            var saved = await _bookingRepo.SaveChangesAsync();

            if (!saved)
                return BadRequest("Gagal memperbarui data booking.");

            return NoContent();
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetBookingDetail(int id)
        {
            var booking = await _context.PemesananKaraoke
                .Include(p => p.Pelanggan)
                .Include(p => p.PemanduKaraoke)
                .Include(p => p.RuanganKaraoke)
                    .ThenInclude(r => r.JenisRuanganKaraoke)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (booking == null)
                return NotFound("Data booking tidak ditemukan.");

            var result = new
            {
                Id = booking.Id,
                NamaPelanggan = booking.Pelanggan?.Nama,
                NamaPemandu = booking.PemanduKaraoke?.NamaPemandu,
                NomorRuangan = booking.RuanganKaraoke?.NomorRuangan,
                JenisRuangan = booking.RuanganKaraoke?.JenisRuanganKaraoke?.NamaJenisRuangan,
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

