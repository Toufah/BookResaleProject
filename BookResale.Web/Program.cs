using Blazored.LocalStorage;
using BookResale.Web;
using BookResale.Web.Pages;
using BookResale.Web.Services;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BookResale.Web.Shared.Providers;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7133/") });

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IFilterService, FilterService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISellerBankAccountInfo, SellerBankAccountInfo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInboxService, InboxService>();
builder.Services.AddScoped<FilesManager>();


await builder.Build().RunAsync();