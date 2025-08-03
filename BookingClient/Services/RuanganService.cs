using BookingClient.Contract;
using BookingClient.DTOs;
using System.Text.Json;

namespace BookingClient.Services
{
    public class RuanganService : IRuanganService
    {
        private readonly HttpClient _httpClient;

        public RuanganService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RuanganDTO> GetRuanganByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7299/api/RuanganKaraoke/detail/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Gagal mengambil data ruangan");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<RuanganDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
