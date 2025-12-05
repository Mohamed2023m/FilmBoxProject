using FilmBox.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.Services
{
    public class UserApiService
    {
        private readonly HttpClient _http;

        public UserApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResult?> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/User/login", new
            {
                email,
                password
            });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthResult>();
        }
    }

}