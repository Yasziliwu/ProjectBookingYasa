using BookingClient.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingClient.Pages
{
    public class KaraokeModel : PageModel
    {
        public List<PilihRuanganKaraokeDTO> RuanganList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            var id = HttpContext.Session.GetInt32("id");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Login");
            }

            using var http = new HttpClient();
            RuanganList = await http.GetFromJsonAsync<List<PilihRuanganKaraokeDTO>>("https://localhost:7299/api/PilihRuanganKaraoke");

            return Page();

        }
    }
}
