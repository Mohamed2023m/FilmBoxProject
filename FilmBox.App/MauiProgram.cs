using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FilmBox.App.Services;
using FilmBox.App.ViewModel;



namespace FilmBox.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

        // builder.Services.AddHttpClient();


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<ReviewApiService>();
        builder.Services.AddTransient<ReviewViewModel>();
        builder.Services.AddTransient<StubMediaService>();
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7070/") });
 
        return builder.Build();
	}
}
