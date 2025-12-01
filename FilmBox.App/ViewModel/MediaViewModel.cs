using FilmBox.API.DTOs.GetDTOs;
using FilmBox.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmBox.App.ViewModel
{
    public class MediaViewModel
    {
        private readonly IMediaService _mediaService;
        public ObservableCollection<MediaDto> Medias { get; private set; }



        public MediaViewModel(IMediaService mediaService)
        {

            _mediaService = mediaService;

            Medias = new ObservableCollection<MediaDto>();
        }


        public async Task LoadAsync()
        {
            var data = await _mediaService.GetAllMedia();

            Medias.Clear();

            foreach (var item in data)
            {

                Medias.Add(item);

            }

        }

       
    }
}
