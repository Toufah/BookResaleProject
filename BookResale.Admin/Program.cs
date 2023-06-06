using Blazored.LocalStorage;
using Blazored.Toast;
using BookResale.Admin;
using BookResale.Admin.Services.StatsServices;
using BookResale.Web.Shared.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7133/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
