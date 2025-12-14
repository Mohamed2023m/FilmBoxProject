using FilmBox.API.DTOs.GetDTOs;

namespace FilmBox.API.BusinessLogic.Interfaces
{
    public interface IMediaLogic
    {

        public Task<MediaDto> GetMediaById(int Id);

        public Task<IEnumerable<MediaDto>> GetAllFilms();

        public Task<IEnumerable<MediaDto>> GetAllSeries();

        public  Task<IEnumerable<MediaDto>> GetRecentlyAddedMedia();

        public  Task<MediaDto> GetMediaImageById(int Id);

    }
}
