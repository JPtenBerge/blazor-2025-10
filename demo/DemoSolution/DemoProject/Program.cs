using DemoProject.Components;
using DemoProject.DataAccess;
using DemoProject.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<SnackContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SnackContext"));
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// builder.Services.AddSingleton<ISnackRepository, SnackRepository>();
builder.Services.AddTransient<ISnackRepository, SnackDbRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();