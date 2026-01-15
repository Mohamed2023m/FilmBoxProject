using FilmBox.Api.DTOs.PostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FilmBox.App.Services
{
    public class ReviewApiService
    {
        private readonly HttpClient _http;

        public ReviewApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<int> CreateReviewAsync(ReviewCreateDto dto, string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PostAsync("api/reviews", content);
            resp.EnsureSuccessStatusCode();

            var body = await resp.Content.ReadAsStringAsync();

            if (int.TryParse(body.Trim('"'), out var id))
                return id;

            return 0;
        }
    }
}
