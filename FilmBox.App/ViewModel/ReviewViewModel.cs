using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.API.DTOs.GetDTOs;
using FilmBox.App.Services;
using FilmBox.App.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FilmBox.App.ViewModel
{
    public class ReviewViewModel
    {
        private readonly ReviewApiService _api;
        private readonly IMediaService _mediaService;

        public int Rating { get; set; } = 0;
        public string? Description { get; set; }

        public MediaDto? LoadedMedia { get; private set; }
        public string? Message { get; private set; }

        public ReviewViewModel(ReviewApiService api, IMediaService mediaService)
        {
            _api = api;
            _mediaService = mediaService;
        }

        public void ClearMessage() => Message = null;
        public void ClearForm()
        {
            Rating = 0;
            Description = string.Empty;
            Message = null;
        }

        public async Task<bool> LoadMediaAsync(int id)
        {
            Message = null;
            try
            {
                var media = await _mediaService.GetMediaById(id);
                LoadedMedia = media;

                if (LoadedMedia == null)
                {
                    Message = "Medie ikke fundet.";
                    return false;
                }

                ClearForm();


                return true;
            }
            catch (Exception ex)
            {
                Message = "Fejl ved hentning af medie: " + ex.Message;
                return false;
            }
        }

        public async Task<bool> SubmitReviewAsync()
        {
            Message = null;

            if (LoadedMedia == null)
            {
                Message = "Intet medie valgt";
                return false;
            }

            if (Rating < 1 || Rating > 5)
            {
                Message = "Vælg rating 1-5!";
                return false;
            }

            var dto = new ReviewCreateDto
            {
                MediaId = LoadedMedia.Id,
                Rating = Rating,
                Description = Description
            };

            try
            {
                var token = await SecureStorage.Default.GetAsync("jwt");
                if (string.IsNullOrEmpty(token))
                {
                    Message = "Ingen token fundet. Log ind først!";
                    return false;
                }

                var id = await _api.CreateReviewAsync(dto, token);

                if (id > 0)
                    Message = $"Review oprettet med ID: {id}";
                else
                    Message = "Review blev ikke oprettet.";

                return id > 0;
            }
            catch (Exception ex)
            {
                Message = "Fejl: " + ex.Message;
                return false;
            }
        }
    }
}