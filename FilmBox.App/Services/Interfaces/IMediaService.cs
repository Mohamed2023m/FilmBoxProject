using FilmBox.API.DTOs.GetDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.Services.Interfaces
{
    public interface IMediaService
    {
        public Task<IEnumerable<MediaDto>> GetAllMedia();

    }
}
