using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;

namespace FilmBox.Api.BusinessLogic
{
    // Business logic operations allowed for reviews
    public interface IReviewLogic
    {
        // Creates a new review for a specific user.
        Task<int> CreateReviewAsync(int? userId, ReviewCreateDto dto);
        Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId);


    }
}
