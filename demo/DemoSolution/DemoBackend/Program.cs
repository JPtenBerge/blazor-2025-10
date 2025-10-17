using System.Security.Claims;
using Demo.Shared.Auth;
using Demo.Shared.Repositories;
using DemoBackend.DataAccess;
using DemoBackend.Endpoints;
using DemoBackend.Repositories;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SnackContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SnackContext"));
});
builder.Services.AddTransient<ISnackRepository, SnackDbRepository>();

builder.Services.AddMemoryCache();

// builder.Services.AddDistributedMemoryCache()
// Redis

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = "https://localhost:5001";
    options.TokenValidationParameters = new()
    {
        ValidateAudience = false, // tijdelijk
        ValidateIssuer = false,
        NameClaimType = JwtClaimTypes.Name
    };
});

// HTTP-header  Authorization: Bearer ey...

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BobPolicy", policy => policy.Requirements.Add(new BobAuthorRequirement()));

    options.AddPolicy("alleencoolemensen", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("name", "Bob Smith");
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, BobAuthorizationHandler>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Use(async (context, next) =>
{
    if (context.Request.Headers.TryGetValue("Authorization", out var authorization))
    {
        Console.WriteLine($"auth header: {authorization}");
    }
    else
    {
        Console.WriteLine("geen auth header");
    }

    await next();
});

app.UseHttpsRedirection();

app.MapSnackEndpoints();

app.Run();