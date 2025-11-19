using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using System.Reflection;

namespace FilmBox.Api.Utils
{
    //The class is converting between DTOs and domain models.
    public static class DTOConverter
    {
        // Converts a ReviewCreateDto into a Review domain model (From the client)
        public static Review FromCreateDto(this ReviewCreateDto dto, int? userId = null)
    => new Review { Rating = dto.Rating, Description = dto.Description, MediaId = dto.MediaId, UserId = userId };

        //Converts a Review domain model into a ReviewDto (To the client)
        public static ReviewDto ToDto(this Review r)
               => new ReviewDto
               {
                   ReviewId = r.ReviewId,
                   CreatedAt = r.CreatedAt,
                   Rating = r.Rating,
                   Description = r.Description,
                   MediaId = r.MediaId,
                   UserId = r.UserId
               };

        public static IEnumerable<ReviewDto> ToDtos(this IEnumerable<Review> reviews)
        {
            var list = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                list.Add(review.ToDto());
            }

            return list;
        }
    }
}
