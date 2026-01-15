using System.ComponentModel.DataAnnotations;

namespace FilmBox.Api.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }    

        public string? Description { get; set; }

        [Required]
        public int MediaId { get; set; } 
        public int? UserId { get; set; }

        // Navigation
        public Media? Media { get; set; }
        public User? User { get; set; }




    }
}
