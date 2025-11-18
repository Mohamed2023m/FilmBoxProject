using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;

namespace FilmBox.Api.BusinessLogic
{
    // Business logic operations allowed for reviews
    public interface IReviewService
    {
        // Creates a new review for a specific user.
        Task<int> CreateReviewAsync(int? userId, ReviewCreateDto dto);

   
    }
}
