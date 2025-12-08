using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.ViewModel
{
    public class ReviewViewModel
    {
        private readonly ReviewApiService _api;

        public int MediaId { get; set; }
        public int Rating { get; set; } = 0;
        public string? Description { get; set; }

        public ReviewViewModel(ReviewApiService api)
        {
            _api = api;
        }

        public async Task<int> CreateReviewAsync()
        {
            var dto = new ReviewCreateDto
            {
                MediaId = MediaId,
                Rating = Rating,
                Description = Description
            };

            // midlertidigt fake jwt — senere fra login
            string fakeJwt = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhYmRpQGV4YW1wbGUuY29tIiwidXNlcklkIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3NjQ3NjgzMTIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMzI7IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzAzMjsifQ.gms7q8bpoOlY7wyEus4Fn3jelAERhz6dlIUMxgaiJVP2ZQhdizu5BWylFWv-EFcbl64WFqa2FLe9X-RHLR4XK1xl1_5txfNmb9IRV9xrtOlL28G6RpQVpm7HTxke0D1kOLDmPr65JrwNlDWtBFcMVStZcWk_lp_liT8rLpM-IRjMm8ifPgGlLHD_iI164siYq2tdfQllsnR0uOW5OP_AKXFDiTKxMsuJ8ye7iKsCRRCibzA88jJwGJgLieg44zWNHnI_7cUyDpZHJzdU_XaYAb74I6m-PJUsuYQXOplq1wIDJ17LkCyziPr_Y_cgdUdPdcLCij2gBo3pdNCfHaJ1ig";

            var createdId = await _api.CreateReviewAsync(dto, fakeJwt);
            return createdId;
        }
    }
}
