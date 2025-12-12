using FilmBox.API.DTOs.GetDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.Shared.DTOs.GetDTOs
{
    public class MovieDto : MediaDto
    {
        public int DurationMinutes { get; set; }
    }
}
