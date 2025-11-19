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
        public DateTime? PublishDate { get; set; }

        // Navigation 
        public Movie? Movie { get; set; }
        public Series? Series { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
