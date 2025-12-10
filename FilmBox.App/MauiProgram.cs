using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FilmBox.App.Services;
using FilmBox.App.Services.Interfaces;
using FilmBox.App.ViewModel;
using FilmBox.App.ViewModels;

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

        builder.Services.AddHttpClient();


       // builder.Services.AddHttpClient<UserApiService>(client =>
       // {
      //      client.BaseAddress = new Uri("https://localhost:7070"); 
     //   });

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7070/") });


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        
        builder.Services.AddScoped<LoginViewModel>();
        builder.Services.AddScoped<ReviewViewModel>();
        builder.Services.AddTransient<MediaViewModel>();
        builder.Services.AddTransient<MediaDetailViewModel>();

        builder.Services.AddTransient<IMediaService, MediaService>();
        builder.Services.AddScoped<UserApiService>();
        builder.Services.AddScoped<ReviewApiService>();
        builder.Services.AddScoped<StubMediaService>(); 
        return builder.Build();
	}
}
