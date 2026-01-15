using System.ComponentModel.DataAnnotations;

namespace FilmBox.Api.Models
{
    public class Series
    {

        public int MediaId { get; set; }

        public int? TotalSeasons { get; set; }
        public bool Airing { get; set; } = false;

        public Media? Media { get; set; }
    }

}

