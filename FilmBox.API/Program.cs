using FilmBox.Api.Authentication;
using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DATA ACCESS
builder.Services.AddSingleton<IUserAccess>(new UserAccess(connectionString));

// BUSINESS LOGIC
builder.Services.AddScoped<UserLogic>();

// JWT Token Generator
builder.Services.AddSingleton<JwtTokenGenerator>();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var privateKeyPath = builder.Configuration["Jwt:PrivateKeyPath"];
        var privateKeyXml = File.ReadAllText(privateKeyPath);

        var rsa = RSA.Create();
        rsa.FromXmlString(privateKeyXml); // contains public key inside

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new RsaSecurityKey(rsa)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADD AUTHENTICATION BEFORE AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
