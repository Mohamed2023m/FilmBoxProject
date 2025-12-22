using Filmbox.Admin.Auth;
using Filmbox.Admin.Components;

using Microsoft.AspNetCore.Components.Authorization;
using Filmbox.Admin.Services;

namespace Filmbox.Admin;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();


         builder.Services.AddHttpClient<AdminUserApiService>(client =>
         {
              client.BaseAddress = new Uri("https://localhost:7070"); 
          });

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7070/") });

        builder.Services.AddScoped<AdminMediaService>();
        builder.Services.AddScoped<JwtTokenStore>();
        builder.Services.AddScoped<AdminAuthenticationStateProvider>();

        builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<AdminAuthenticationStateProvider>());

        builder.Services.AddAuthorizationCore();
        builder.Services.AddAuthorization();
        builder.Services.AddCascadingAuthenticationState();

        var app = builder.Build();

        // Error handling
        if (!app.Environment.IsDevelopment())
        {
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

