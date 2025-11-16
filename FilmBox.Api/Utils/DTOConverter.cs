using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;

namespace FilmBox.Api.Utils
{
    public static class DTOConverter
    {
        public static Review FromCreateDto(this ReviewCreateDto dto, int? userId = null)
    => new Review { Rating = dto.Rating, Description = dto.Description, MediaId = dto.MediaId, UserId = userId };

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

        //public static IEnumerable<ReviewDto> ToDtos(this IEnumerable<Review> reviews)
        //{
        //    foreach (var r in reviews) yield return r.ToDto();
        //}

        
    }
}
