using FilmBox.Api.Models;
using FilmBox.API.DTOs.GetDTOs;

namespace FilmBox.API.DataAccess.Interfaces
{
    public interface IMediaAccess
    {

        public  Task<Media?> FetchMediaAsync(int id);

        public Task<IEnumerable<Media>> FetchAllSeriesAsync();

        public Task<IEnumerable<Media>> FetchAllFilmsAsync();

        public Task<IEnumerable<Media>> FetchRecentlyAddedMedia();

        public  Task<Media?> FetchMediaImageById(int id);

    }
}
