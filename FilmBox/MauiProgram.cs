using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace FilmBox
{
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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // -------------------------------
            // Add HttpClient for API
            // -------------------------------
            builder.Services.AddHttpClient("FilmBoxAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/"); // your WebAPI URL
            });

      

            return builder.Build();
        }
    }
}
