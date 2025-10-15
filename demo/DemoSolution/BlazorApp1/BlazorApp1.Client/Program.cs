using BlazorApp1.Client;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ISnackRepository, SnackRepository>();

await builder.Build().RunAsync();