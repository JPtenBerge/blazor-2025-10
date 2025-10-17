using Demo.Shared.Repositories;
using DemoProject.Components;
using DemoProject.DataAccess;
using DemoProject.Repositories;
using Duende.Bff.Blazor;
using Duende.Bff.Yarp;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<SnackContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SnackContext"));
});

builder.Services.AddBff()
    .AddServerSideSessions() // Add in-memory implementation of server side sessions
    .AddBlazorServer();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddBffExtensions();;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";

        // Because we use an identity server that's configured on a different site
        // (duendesoftware.com vs localhost), we need to configure the SameSite property to Lax. 
        // Setting it to Strict would cause the authentication cookie not to be sent after loggin in.
        // The user would have to refresh the page to get the cookie.
        // Recommendation: Set it to 'strict' if your IDP is on the same site as your BFF.
        options.Cookie.SameSite = SameSiteMode.Lax;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "blazorfrontend";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.MapInboundClaims = false;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("scope2");
        options.Scope.Add("offline_access");

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
    });

// Make sure authentication state is available to all components. 
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthorization();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("frontend", policy =>
//     {
//         policy.WithOrigins("http://localhost:5075").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//     });
// });

// builder.Services.AddSingleton<ISnackRepository, SnackRepository>();
builder.Services.AddTransient<ISnackRepository, SnackDbRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapReverseProxy();

// app.UseCors("frontend");

app.UseAntiforgery();

app.MapBffManagementEndpoints();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp1.Client._Imports).Assembly);

app.Run();