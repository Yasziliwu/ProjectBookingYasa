using BookingClient.DTOs;
using BookingClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingClient.Pages
{
    public class BookingPijatModel : PageModel
    {
        private readonly BookingPijatService _bookingService;

        public BookingPijatModel(BookingPijatService bookingService)
        {
            _bookingService = bookingService;
        }

        [BindProperty]
        public BookingPijatDTO Pemesanan { get; set; } = new();

        public List<TerapisDTO> TerapisList { get; set; } = new();
        public LayananPijatDTO LayananPijat { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            TerapisList = await _bookingService.GetAllTerapisAsync();

            if (id.HasValue)
            {
                Pemesanan.LayananPijat = await _bookingService.GetLayananByIdAsync(id.Value);
                Pemesanan.LayananPijatId = id.Value;
            }

            if (Pemesanan.TanggalPemesanan == default)
                Pemesanan.TanggalPemesanan = DateTime.Today;

            if (Pemesanan.WaktuMulai == default)
                Pemesanan.WaktuMulai = Pemesanan.TanggalPemesanan.Date + TimeSpan.FromHours(9);

            if (Pemesanan.WaktuSelesai == default)
                Pemesanan.WaktuSelesai = Pemesanan.TanggalPemesanan.Date + TimeSpan.FromHours(10);

            if (Pemesanan.CabangUsahaId == 0)
                Pemesanan.CabangUsahaId = 2;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var pelangganId = HttpContext.Session.GetInt32("id");
            if (pelangganId == null)
            {
                ViewData["SwalError"] = "Session pelanggan tidak ditemukan. Silakan login kembali.";
                await LoadPostPageAsync();
                return Page();
            }


            if (Pemesanan.TerapisId == 0)
            {
                ViewData["SwalError"] = "Silakan pilih terapis terlebih dahulu.";
                await OnGetAsync(Pemesanan.LayananPijatId); // pastikan layanan pijat tetap terload
                return Page();
            }

            var tanggal = Pemesanan.TanggalPemesanan.Date;
            var waktuMulai = Pemesanan.WaktuMulai;
            var waktuSelesai = Pemesanan.WaktuSelesai;

            // ✅ Validasi backdate
            if (tanggal < DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "Tanggal pemesanan tidak boleh di masa lalu.");
                ViewData["SwalError"] = "Waktu Selesai tidak boleh kurang dari Waktu mulai.";
                await LoadPostPageAsync();
                return Page();
            }

            if (waktuSelesai <= waktuMulai)
            {
                ModelState.AddModelError(string.Empty, "Waktu selesai harus setelah waktu mulai.");
                ViewData["SwalError"] = "Waktu Selesai tidak boleh kurang dari Waktu mulai.";
                await LoadPostPageAsync();
                return Page();
            }

            try
            {
                var submitDto = new BookingPijatSubmitDTO
                {
                    PelangganId = pelangganId.Value,
                    CabangUsahaId = 2,
                    LayananPijatId = Pemesanan.LayananPijatId,
                    TerapisId = Pemesanan.TerapisId,
                    TanggalPemesanan = Pemesanan.TanggalPemesanan.Date,
                    WaktuMulai = waktuMulai,
                    WaktuSelesai = waktuSelesai
                };

                var result = await _bookingService.CreateBookingAsync(submitDto);

                TempData["SuccessMessage"] = $"Booking berhasil! ID: {result.BookingId}, Total harga: {result.TotalHarga:C}";
                return RedirectToPage("BookingPijatSuccess");
            }
            catch (Exception ex)
            {
                ViewData["SwalError"] = $"Gagal melakukan booking: {ex.Message}";
                await LoadPostPageAsync();
                return Page();
            }
        }

        private async Task LoadPostPageAsync()
        {
            TerapisList = await _bookingService.GetAllTerapisAsync();

            if (Pemesanan.LayananPijatId > 0)
            {
                Pemesanan.LayananPijat = await _bookingService.GetLayananByIdAsync(Pemesanan.LayananPijatId);
            }

            if (Pemesanan.TanggalPemesanan == default)
                Pemesanan.TanggalPemesanan = DateTime.Today;

            if (Pemesanan.WaktuMulai == default)
                Pemesanan.WaktuMulai = Pemesanan.TanggalPemesanan.Date + TimeSpan.FromHours(9);

            if (Pemesanan.WaktuSelesai == default)
                Pemesanan.WaktuSelesai = Pemesanan.TanggalPemesanan.Date + TimeSpan.FromHours(10);
        }
    }
}


