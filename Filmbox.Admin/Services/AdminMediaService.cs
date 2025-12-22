using Filmbox.Admin.Models;
using FilmBox.Shared.DTOs.PostDTOs;
using System.Net.Http.Headers;

namespace Filmbox.Admin.Services
{

    public class AdminMediaService
    {
        private readonly HttpClient _http;
        private const string AdminToken =
      "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbkBmaWxtYm94LmRrIiwidXNlcklkIjoiMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzY2MTkxOTg3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDMyOyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMzI7In0.ESLx0gQEoWI7HPKL6yYmWsx4E7ukAxwf8bVFzY46MQUknU2qtUXfG3JiqZCxe7jpLPdZ2cR8wgGSOeBW4fMFr1BPRQoM5DIbYrebVSvVMDTxQwXCD2PO1X7YUOSnKJmGe-RrogCFzapKOpT7Jx2kgUsT9WXJmfgoXQDe-lp-oGf-4TyRvdMuM0YuCugu0k8px6Mvky-cUIAMTWU8ztHh9mWQJ8PW3aJxeBANSY0edN3zD2ij4sUoEKf0AhSDqtc4kY0_9DAXSwTNOOxOfp58u5xz4HdGijgYv6ICabHqeB9CntBTya1bt-hymHZsmuOYED6KhGhXWUwfO7BqXHOWBQ";
        public AdminMediaService(HttpClient http)
        {
            _http = http;
        }

        public async Task CreateMediaAsync(MediaCreateFormModel model)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AdminToken);
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

