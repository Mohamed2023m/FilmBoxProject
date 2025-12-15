using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using FilmBox.Api.Authentication;
using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DataAccess;
using FilmBox.API.BusinessLogic;
using FilmBox.API.BusinessLogic.Interfaces;
using FilmBox.API.DataAccess;
using FilmBox.API.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Indsæt kun dit JWT-token her",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});

// Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DATA ACCESS
builder.Services.AddSingleton<IUserDAO>(new UserDAO(connectionString));
builder.Services.AddSingleton<IReviewDAO>(new ReviewDAO(connectionString));

// Incoming DAOs / Repositories

builder.Services.AddScoped<IMediaAccess, MediaAccess>();

// BUSINESS LOGIC
builder.Services.AddScoped<UserLogic>();
builder.Services.AddScoped<IReviewLogic, ReviewLogic>();

// Incoming Business Logic / Services

builder.Services.AddScoped<IMediaLogic, MediaLogic>();

// JWT Token Generator
builder.Services.AddSingleton<JwtTokenGenerator>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var privateKeyPath = builder.Configuration["Jwt:PrivateKeyPath"];
        var privateKeyXml = File.ReadAllText(privateKeyPath);

        var rsa = RSA.Create();
        rsa.FromXmlString(privateKeyXml);

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireRole("Admin"));
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ADD AUTHENTICATION BEFORE AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
