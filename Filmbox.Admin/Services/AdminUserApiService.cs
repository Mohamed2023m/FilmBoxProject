using FilmBox.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Filmbox.Admin.Services
{
    public class AdminUserApiService
    {
        private readonly HttpClient _http;

        public AdminUserApiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<AuthResult?> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/User/login", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AuthResult>();
        }
    }

}