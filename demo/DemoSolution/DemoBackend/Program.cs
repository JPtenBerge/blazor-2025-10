using Demo.Shared.Repositories;
using DemoBackend.DataAccess;
using DemoBackend.Endpoints;
using DemoBackend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SnackContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SnackContext"));
});
builder.Services.AddTransient<ISnackRepository, SnackDbRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapSnackEndpoints();

app.Run();