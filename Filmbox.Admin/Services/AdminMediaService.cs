using Filmbox.Admin.Auth;
using Filmbox.Admin.Models;
using FilmBox.Shared.DTOs.PostDTOs;
using System.Net.Http.Headers;

namespace Filmbox.Admin.Services
{

    public class AdminMediaService
    {
        private readonly HttpClient _http;
        private readonly JwtTokenStore _tokenStore;
        public AdminMediaService(HttpClient http, JwtTokenStore tokenStore)
        {
            _http = http;
            _tokenStore = tokenStore;

        }

        public async Task CreateMediaAsync(MediaCreateFormModel model)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _tokenStore.Token);
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Title), "Title");
            content.Add(new StringContent(model.Description ?? ""), "Description");
            content.Add(new StringContent(model.Genre ?? ""), "Genre");
            content.Add(new StringContent(model.MediaType), "MediaType");

            if (model.PublishDate.HasValue)
            {
                content.Add(
                    new StringContent(model.PublishDate.Value.ToString("yyyy-MM-dd")),
                    "PublishDate"
                );
            }

            if (model.ImageUrl != null)
            {
                var file = new StreamContent(model.ImageUrl.OpenReadStream());
                file.Headers.ContentType =
                    new MediaTypeHeaderValue(model.ImageUrl.ContentType);

                content.Add(file, "ImageUrl", model.ImageUrl.Name);
            }

            var response = await _http.PostAsync("api/AdminMedia", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API error: {response.StatusCode} - {error}");
            }
        }
    }
}

