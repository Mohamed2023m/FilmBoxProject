using FilmBox.Api.Authentication;
using FilmBox.Api.BusinessLogic;
using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Indsæt kun dit JWT-token her ",
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

// BUSINESS LOGIC
builder.Services.AddScoped<UserLogic>();
builder.Services.AddScoped<IReviewLogic, ReviewLogic>();

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

builder.Services.AddAuthorization(options =>
{
    // Eksempel: krav om authentication som default for alle endpoints
    // options.FallbackPolicy = new AuthorizationPolicyBuilder()
    //     .RequireAuthenticatedUser()
    //     .Build();

    // Eksempel policy
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

// ADD AUTHENTICATION BEFORE AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
