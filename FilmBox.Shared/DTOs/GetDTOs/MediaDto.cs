using System;

namespace FilmBox.API.DTOs.GetDTOs
{
    public class MediaDto
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? PublishDate { get; set; }

        public string MediaType { get; set; }


        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
