using System.ComponentModel.DataAnnotations;

namespace FilmBox.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string? Email { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }

        // Navigation 
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
