using BookingClient.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BookingClient.Pages
{
    public class SuccessModel : PageModel
    {
        public BookingDetailDTO BookingDetail { get; set; }
        public string VirtualAccount { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            var bookingId = HttpContext.Session.GetInt32("BookingId");
            if (bookingId == null) return RedirectToPage("/Index");


            var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7299/api/BookingKaraoke/detail/{bookingId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Index");

            var json = await response.Content.ReadAsStringAsync();
            BookingDetail = JsonSerializer.Deserialize<BookingDetailDTO>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            VirtualAccount = GenerateVirtualAccount(bookingId.ToString());
            return Page();
        }

        private string GenerateVirtualAccount(string id)
        {
            return $"8888{id.PadLeft(9, '0')}";
        }
    }
}
