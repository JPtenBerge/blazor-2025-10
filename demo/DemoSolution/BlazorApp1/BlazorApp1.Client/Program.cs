using System.Net.Http.Json;
using BlazorApp1.Client;
using BlazorApp1.Client.Repositories;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ISnackRepository, SnackRepository>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();