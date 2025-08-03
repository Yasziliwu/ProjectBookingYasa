using BookingClient.DTOs;
using BookingClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingClient.Pages
{
    public class BookingPijatSuccessModel : PageModel
    {
        private readonly BookingPijatService _bookingService;

        public BookingPijatDetailDTO BookingDetail { get; set; }
        public string VirtualAccount { get; set; } = string.Empty;

        public BookingPijatSuccessModel(BookingPijatService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var bookingId = HttpContext.Session.GetInt32("BookingId");
            if (bookingId == null) return RedirectToPage("/Index");

            try
            {
                BookingDetail = await _bookingService.GetBookingDetailAsync(bookingId.Value);
                VirtualAccount = GenerateVirtualAccount(bookingId.ToString());
            }
            catch
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        private string GenerateVirtualAccount(string id)
        {
            return $"9999{id.PadLeft(9, '0')}";
        }
    }
}
