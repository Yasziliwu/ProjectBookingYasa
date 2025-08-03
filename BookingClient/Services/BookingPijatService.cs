using BookingClient.DTOs;

namespace BookingClient.Services
{
    public class BookingPijatService
    {
        private readonly HttpClient _httpClient;

        public BookingPijatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TerapisDTO>> GetAllTerapisAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TerapisDTO>>("api/terapis");
        }

        public async Task<LayananPijatDTO> GetLayananByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<LayananPijatDTO>($"api/layananpijat/{id}");
        }

        public async Task<PijatBookingResponseDTO> CreateBookingAsync(BookingPijatSubmitDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookingpijat", dto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PijatBookingResponseDTO>();
                return result;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Booking gagal: {error}");
            }
        }

        public async Task<BookingPijatDetailDTO> GetBookingDetailAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/bookingpijat/detail/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Gagal mengambil detail booking.");

            var result = await response.Content.ReadFromJsonAsync<BookingPijatDetailDTO>();
            return result!;
        }
    }
}
