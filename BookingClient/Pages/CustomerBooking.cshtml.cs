using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using BookingClient.DTOs;
using System.Reflection;
using BookingClient.Contract;

namespace BookingClient.Pages
{
    public class CustomerBookingModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRuanganService _ruanganService;

        public CustomerBookingModel(IHttpClientFactory httpClientFactory, IRuanganService ruanganService)
        {
            _httpClientFactory = httpClientFactory;
            _ruanganService = ruanganService;
        }

        [BindProperty]
        public BookingKaraokeDTO Booking { get; set; } = new();
        [BindProperty]
        public string NamaRuangan { get; set; } = "";
        public decimal HargaRuangan { get; set; }
        public int Id { get; set; }
        public List<PemanduKaraokeDTO> PemanduList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id, string room)
        {
            var email = HttpContext.Session.GetString("email");
            var idPelanggan = HttpContext.Session.GetInt32("id");

            if (string.IsNullOrEmpty(email) || idPelanggan == null)
                return RedirectToPage("/Login");

            Booking.RuanganId = id;
            Booking.IdPelanggan = idPelanggan.Value;
            NamaRuangan = room;

            try
            {
                var client = _httpClientFactory.CreateClient();
                var result = await client.GetAsync("https://localhost:7299/api/PemanduKaraoke");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    PemanduList = JsonSerializer.Deserialize<List<PemanduKaraokeDTO>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new();
                }

                var ruangan = await _ruanganService.GetRuanganByIdAsync(id);
                HargaRuangan = ruangan.HargaPerJam;
            }
            catch (Exception ex)
            {
                ViewData["SwalError"] = $"Gagal memuat data: {ex.Message}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var idPelanggan = HttpContext.Session.GetInt32("id");
            if (idPelanggan == null)
            {
                ViewData["SwalError"] = "Session pelanggan tidak ditemukan.";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }
            Booking.IdPelanggan = idPelanggan.Value;

            var ruanganIdQuery = Request.Query["id"];
            if (!int.TryParse(ruanganIdQuery, out int ruanganId))
            {
                ViewData["SwalError"] = "Ruangan tidak valid.";
                return Page();
            }
            Booking.RuanganId = ruanganId;

            if (Booking.PemanduId == 0)
            {
                ViewData["SwalError"] = "Silakan pilih pemandu karaoke.";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }

            if (Booking.WaktuMulai == default)
                Booking.WaktuMulai = Booking.TanggalPemesanan.Date.AddHours(19);

            if (Booking.WaktuSelesai == default)
                Booking.WaktuSelesai = Booking.WaktuMulai.Add(TimeSpan.FromHours(2));

            // ✅ Validasi backdate
            if (Booking.TanggalPemesanan.Date < DateTime.Today)
            {
                ViewData["SwalError"] = "Tanggal pemesanan tidak boleh di masa lalu.";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }

            // ✅ Validasi waktu selesai harus setelah waktu mulai
            if (Booking.WaktuSelesai <= Booking.WaktuMulai)
            {
                ViewData["SwalError"] = "Waktu selesai harus lebih dari waktu mulai.";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }

            try
            {
                var ruangan = await _ruanganService.GetRuanganByIdAsync(Booking.RuanganId);
                HargaRuangan = ruangan.HargaPerJam;

                var totalJam = (Booking.WaktuSelesai - Booking.WaktuMulai).TotalHours;
                Booking.TotalHarga = HargaRuangan * (decimal)totalJam;
                Booking.StatusPemesanan = "Pending";

                var client = _httpClientFactory.CreateClient();
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(Booking),
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("https://localhost:7299/api/BookingKaraoke", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    using var jsonDoc = JsonDocument.Parse(responseBody);

                    if (jsonDoc.RootElement.TryGetProperty("id", out var idElement))
                        HttpContext.Session.SetInt32("BookingId", idElement.GetInt32());

                    return RedirectToPage("/Success");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewData["SwalError"] = $"Gagal booking: {errorMessage}";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["SwalError"] = $"Terjadi kesalahan saat memproses booking: {ex.Message}";
                await LoadPageDataAsync(Booking.RuanganId);
                return Page();
            }
        }

        private async Task LoadPageDataAsync(int ruanganId)
        {
            var client = _httpClientFactory.CreateClient();

            // Ambil pemandu
            var result = await client.GetAsync("https://localhost:7299/api/PemanduKaraoke");
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                PemanduList = JsonSerializer.Deserialize<List<PemanduKaraokeDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
            }

            // Ambil harga ruangan
            var ruangan = await _ruanganService.GetRuanganByIdAsync(ruanganId);
            HargaRuangan = ruangan.HargaPerJam;
        }
    }
}
