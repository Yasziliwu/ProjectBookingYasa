using BookingClient.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingClient.Pages
{
    public class PijatModel : PageModel
    {
        public List<LayananPijatDTO> LayananList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Login");
            }

            using var http = new HttpClient();
            var response = await http.GetFromJsonAsync<List<LayananPijatDTO>>("https://localhost:7299/api/LayananPijat");

            if (response != null)
            {
                LayananList = response;
            }

            return Page();
        }
    }
}

