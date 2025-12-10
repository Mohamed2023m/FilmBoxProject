using FilmBox.API.DTOs.GetDTOs;
using FilmBox.App.Services.Interfaces;
using System.ComponentModel;

namespace FilmBox.App.ViewModel
{
    public class MediaDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IMediaService _mediaService;

        private MediaDto _dto;
        public MediaDto Dto
        {
            get => _dto;
            set
            {
                if (_dto != value)
                {
                    _dto = value;
                    OnPropertyChanged(nameof(Dto));
                }
            }
        }

        public MediaDetailViewModel(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        // Load by ID
        public async Task LoadAsync(int id)
        {
            Dto = await _mediaService.GetMediaById(id);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
