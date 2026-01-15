using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.Shared.DTOs.PostDTOs
{
    public class MediaCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public IFormFile ImageUrl { get; set; }

        [Required]
        public string MediaType { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}
