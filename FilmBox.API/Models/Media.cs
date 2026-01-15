using System.ComponentModel.DataAnnotations;

namespace FilmBox.Api.Models
{
    public class Media
    {
        public int MediaId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        [MaxLength(512)]
        public string? ImageUrl { get; set; }

        public string? MediaType { get; set; }
        public DateTime? PublishDate { get; set; }

        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }

      

    }
}
