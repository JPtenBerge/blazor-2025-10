using System.Net.Http.Json;
using BlazorApp1.Client;
using BlazorApp1.Client.Repositories;
using Demo.Shared.Entities;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ISnackRepository, SnackRepository>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// var http = new HttpClient();
// http.GetFromJsonAsync<IEnumerable<Snack>>("api/snacks");
//
// var response = await http.PostAsJsonAsync<decimal>("api/snacks", 12m);
// var updatedSnack = response.Content.ReadFromJsonAsync<Snack>();

await builder.Build().RunAsync();