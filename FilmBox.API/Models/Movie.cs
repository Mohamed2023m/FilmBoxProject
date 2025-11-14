namespace FilmBox.Api.Models
{
    public class Movie
    {
        public int MediaId { get; set; }
        public Media? Media { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
