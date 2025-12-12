using System;

namespace FilmBox.Api.DTOs.GetDTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public int MediaId { get; set; }
        public int? UserId { get; set; }
    }
}
