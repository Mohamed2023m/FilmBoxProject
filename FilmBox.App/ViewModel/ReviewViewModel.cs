using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.App.Services;

namespace FilmBox.App.ViewModel
{
    public class ReviewViewModel
    {
        private readonly ReviewApiService _api;
        private readonly StubMediaService _stub;

        public int MediaId { get; set; }
        public int Rating { get; set; } = 1;
        public string? Description { get; set; }

        public MediaModel? LoadedMedia { get; private set; }
        public string? Message { get; private set; }

        public ReviewViewModel(ReviewApiService api, StubMediaService stub)
        {
            _api = api;
            _stub = stub;
        }

        public void LoadStubMedia()
        {
            LoadedMedia = _stub.GetStubMedia(MediaId);

            Message = LoadedMedia == null
                ? "Ingen stub fundet"
                : null;
        }

        public async Task<bool> SubmitReviewAsync()
        {
            Message = null;
            string? token = null;

            if (MediaId <= 0)
            {
                Message = "Ugyldigt MediaId!";
                return false;
            }

            if (Rating < 1 || Rating > 5)
            {
                Message = "Vælg rating 1-5!";
                return false;
            }

            var dto = new ReviewCreateDto
            {
                MediaId = MediaId,
                Rating = Rating,
                Description = Description
            };
            try
            {
                token = await SecureStorage.Default.GetAsync("jwt");

                if (string.IsNullOrEmpty(token))
                {
                    Message = "Ingen token fundet. Log ind først!";
                    return false;
                }

                var id = await _api.CreateReviewAsync(dto, token);


                Message = $"Review oprettet med ID: {id}";
                return true;
            }
            catch (Exception ex)
            {
                Message = "Fejl: " + ex.Message;
                return false;
            }
        }
    }
}