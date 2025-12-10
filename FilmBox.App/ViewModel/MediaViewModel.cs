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
        public ObservableCollection<MediaDto> Series { get; private set; }

        public ObservableCollection<MediaDto> Films { get; private set; }

        

        public MediaViewModel(IMediaService mediaService)
        {

            _mediaService = mediaService;

            Series = new ObservableCollection<MediaDto>();

            Films = new ObservableCollection<MediaDto>();
        }


        public async Task LoadAsync()
        {
            var seriesData = await _mediaService.GetAllSeries();

            var filmsData = await _mediaService.GetAllFilms();

            Series.Clear();
            Films.Clear();
            foreach (var item in seriesData)
            {

                Series.Add(item);

            }

            foreach (var item in filmsData)
            {

                Films.Add(item);


            }



        }

       
    }
}
