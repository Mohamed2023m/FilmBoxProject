using FilmBox.API.DTOs.GetDTOs;
using FilmBox.App.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.Services
{
    public class MediaService : IMediaService
    {
        IHttpClientFactory _httpClientFactory;

        public MediaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<MediaDto>> GetAllSeries()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = "http://localhost:5002/api/media/All-Series";

            var response = await httpClient.GetAsync(url);



            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var mediaList = JsonConvert.DeserializeObject<List<MediaDto>>(responseData);

                return mediaList;
                
            }
            return new List<MediaDto>();
        }


        public async Task<MediaDto> GetMediaImageById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5002/api/media/Get-MediaImage/{id}"; // pass ID to API

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var media = JsonConvert.DeserializeObject<MediaDto>(responseData);
                return media;
            }

            return null; // or throw exception depending on your needs
        }


        public async Task<MediaDto> GetMediaById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5002/api/media/Get-Media/{id}"; // pass ID to API

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var media = JsonConvert.DeserializeObject<MediaDto>(responseData);
                return media;
            }

            return null; // or throw exception depending on your needs
        }


        public async Task<IEnumerable<MediaDto>> GetAllFilms()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = "http://localhost:5002/api/media/All-Films";

            var response = await httpClient.GetAsync(url);



            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var mediaList = JsonConvert.DeserializeObject<List<MediaDto>>(responseData);

                return mediaList;

            }
            return new List<MediaDto>();
        }


        public async Task<IEnumerable<MediaDto>> GetRecentlyAddedMedia()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = "http://localhost:5002/api/media/Recently-Added";

            var response = await httpClient.GetAsync(url);



            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                var mediaList = JsonConvert.DeserializeObject<List<MediaDto>>(responseData);

                return mediaList;

            }
            return new List<MediaDto>();
        }


    }
}
