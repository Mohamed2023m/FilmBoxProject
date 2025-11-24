using System.ComponentModel.DataAnnotations;

namespace FilmBox.Api.DTOs.PostDTOs
{
    public class ReviewCreateDto
    {
        [Required]
        public int MediaId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength]
        public string Description { get; set; }
    }
}
