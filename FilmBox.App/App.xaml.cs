using FilmBox.App.Services;
using FilmBox.App.Services.Interfaces;

namespace FilmBox.App;

public partial class App : Application
{
    private readonly IMediaService _mediaService;
    public App(IMediaService mediaService)
	{
		InitializeComponent();

        //_mediaService = mediaService;
        //// Trigger your MediaService method here
        //TriggerMediaService();
    }

    //private async void TriggerMediaService()
    //{
    //    try
    //    {


    //        var mediaList = await _mediaService.GetAllMedia(); // <-- set breakpoint here

    //        // Optional: print to Output window
    //        System.Diagnostics.Debug.WriteLine($"Retrieved {mediaList.Count()} media items");
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
    //    }
    //}

    protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "FilmBox.App" };
	}
}
