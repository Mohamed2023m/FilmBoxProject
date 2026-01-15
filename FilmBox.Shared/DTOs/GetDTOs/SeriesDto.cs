using FilmBox.API.DTOs.GetDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.Shared.DTOs.GetDTOs
{
    public class SeriesDto : MediaDto
    {
        public int TotalSeasons { get; set; }
        public bool Airing { get; set; }
    }
}
