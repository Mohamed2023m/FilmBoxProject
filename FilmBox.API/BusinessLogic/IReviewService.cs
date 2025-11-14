using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;

namespace FilmBox.Api.BusinessLogic
{
    public interface IReviewService
    {
        Task<int> CreateReviewAsync(int? userId, ReviewCreateDto dto);

        Task<IEnumerable<ReviewDto>> GetReviewsForMediaAsync(int mediaId);
    }
}
