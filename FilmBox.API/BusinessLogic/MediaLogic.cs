using FilmBox.Api.Models;
using FilmBox.API.BusinessLogic.Interfaces;
using FilmBox.API.DataAccess.Interfaces;
using FilmBox.API.DTOs.GetDTOs;

namespace FilmBox.API.BusinessLogic
{
    public class MediaLogic : IMediaLogic
    {
        private IMediaAccess _mediaAccess;

        public MediaLogic(IMediaAccess mediaAccess)
        {
            _mediaAccess = mediaAccess;
        }


        public async Task<MediaDto> GetMediaById(int Id)
        {


            var Media = await _mediaAccess.FetchMediaAsync(Id);


            if (Media == null)
            {
                throw new InvalidOperationException("Media not found");

            }


            return new MediaDto
            {
            Title = Media.Title,

            Description = Media.Description,

            Genre = Media.Genre,

            ImageUrl = Media.ImageUrl,

            PublishDate = Media.PublishDate,
            };


        }


        public async Task<IEnumerable<MediaDto>> GetAllSeries()
        {


            var mediaList = await _mediaAccess.FetchAllSeriesAsync();


            var mediaDtoList = new List<MediaDto>();

            foreach (var media in mediaList) {

                var dto = new MediaDto
                {
                    Id = media.MediaId,

                    Title = media.Title,

                    ImageUrl = media.ImageUrl,
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

                var dto = new MediaDto
                {
                    Id = media.MediaId,

                    Title = media.Title,

                    ImageUrl = media.ImageUrl,
                };

                mediaDtoList.Add(dto);

            }

            return mediaDtoList;

        }

    }
}
