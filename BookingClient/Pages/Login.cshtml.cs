using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using BookingClient.DTOs;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BookingClient.Pages
{

    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDTO LoginData { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(LoginData),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://localhost:7299/api/Pelanggan/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonSerializer.Deserialize<LoginDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var pelanggan = loginResponse?.Pelanggan;
                var bookingId = loginResponse?.LastBookingId;

                if (pelanggan != null)
                {
                    HttpContext.Session.SetString("email", pelanggan.Email ?? "");
                    HttpContext.Session.SetInt32("id", pelanggan.Id);
                    HttpContext.Session.SetString("nama", pelanggan.NamaPelanggan ?? "");

                    if (bookingId.HasValue)
                    {
                        HttpContext.Session.SetInt32("BookingId", bookingId.Value);
                        return RedirectToPage("/Success");
                    }

                    return RedirectToPage("/Index");
                }
            }

            Message = "Login gagal: Email tidak ditemukan.";
            return Page();
        }
    }

}
