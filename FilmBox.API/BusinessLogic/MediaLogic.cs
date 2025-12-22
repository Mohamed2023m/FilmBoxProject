using FilmBox.Api.Models;
using FilmBox.API.BusinessLogic.Interfaces;
using FilmBox.API.DataAccess.Interfaces;
using FilmBox.API.DTOs.GetDTOs;

namespace FilmBox.API.BusinessLogic
{
    public class MediaLogic : IMediaLogic
    {
        private IMediaAccess _mediaAccess;

        private readonly ILogger<MediaLogic> _logger;

        public MediaLogic(IMediaAccess mediaAccess, ILogger<MediaLogic> logger)
        {
            _mediaAccess = mediaAccess;
            _logger = logger;
        }

        public async Task<MediaDto> GetMediaById(int Id)
        {


            var Media = await _mediaAccess.FetchMediaAsync(Id);


            if (Media == null)
            {
                _logger.LogWarning("Media ID {Id} not found", Id);
                throw new InvalidOperationException("Media not found");
            }

            var imageUrl = $"http://localhost:5002/Files/{Media.ImageUrl}";
            _logger.LogInformation("Media ID {Id} loaded with ImageUrl: {ImageUrl}", Id, imageUrl);



            return new MediaDto
            {
            Id = Media.MediaId,

            Title = Media.Title,

            Description = Media.Description,

            Genre = Media.Genre,

            AverageRating = Media.AverageRating,

            MediaType = Media.MediaType,

            ReviewCount = Media.ReviewCount,

            ImageUrl = $"http://localhost:5002/Files/{Media.ImageUrl}",

            PublishDate = Media.PublishDate,
            };


        }



        public async Task<MediaDto> GetMediaImageById(int Id)
        {


            var Media = await _mediaAccess.FetchMediaImageById(Id);


            if (Media == null)
            {
                throw new InvalidOperationException("Media not found");

            }


            return new MediaDto
            {


                ImageUrl = $"http://localhost:5002/Files/{Media.ImageUrl}"

            };


        }


        public async Task<IEnumerable<MediaDto>> GetAllSeries()
        {


            var mediaList = await _mediaAccess.FetchAllSeriesAsync();


            var mediaDtoList = new List<MediaDto>();

            foreach (var media in mediaList) {

                _logger.LogInformation("Mapping MediaId: {Id}, Title: {Title}, ImageUrl from DB: {DbUrl}",
    media.MediaId, media.Title, media.ImageUrl);

                var dto = new MediaDto
                {
                    Id = media.MediaId,

                    Title = media.Title,

                    ImageUrl = $"http://localhost:5002/Files/{media.ImageUrl}",
                };



                mediaDtoList.Add(dto);

            }

            return mediaDtoList;

        }

        public async Task<IEnumerable<MediaDto>> GetAllFilms()
        {


            var mediaList = await _mediaAccess.FetchAllFilmsAsync();


            var mediaDtoList = new List<MediaDto>();

            foreach (var media in mediaList)
            {

                _logger.LogInformation("Mapping MediaId: {Id}, Title: {Title}, ImageUrl from DB: {DbUrl}",
    media.MediaId, media.Title, media.ImageUrl);

                var dto = new MediaDto
                {
                    Id = media.MediaId,

                    Title = media.Title,

                    ImageUrl = $"http://localhost:5002/Files/{media.ImageUrl}",
                };

                mediaDtoList.Add(dto);

            }

            return mediaDtoList;

        }


        public async Task<IEnumerable<MediaDto>> GetRecentlyAddedMedia()
        {


            var mediaList = await _mediaAccess.FetchRecentlyAddedMedia();


            var mediaDtoList = new List<MediaDto>();

            foreach (var media in mediaList)
            {

                _logger.LogInformation("Mapping MediaId: {Id}, Title: {Title}, ImageUrl from DB: {DbUrl}",
    media.MediaId, media.Title, media.ImageUrl);
                var dto = new MediaDto
                {
                    Id = media.MediaId,

                    Title = media.Title,

                    ImageUrl = $"http://localhost:5002/Files/{media.ImageUrl}",
                };

                mediaDtoList.Add(dto);

            }

            return mediaDtoList;

        }

    }
}
