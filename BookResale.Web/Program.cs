using BookResale.Web;
using BookResale.Web.Pages;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7133/") });

builder.Services.AddScoped<IBookService, BookService>();

await builder.Build().RunAsync();
