using Blazored.LocalStorage;
using Blazored.Toast;
using BookResale.Admin;
using BookResale.Admin.Services.ApprovalStatusService;
using BookResale.Admin.Services.BookService;
using BookResale.Admin.Services.InboxService;
using BookResale.Admin.Services.OrderService;
using BookResale.Admin.Services.StatsServices;
using BookResale.Admin.Services.UserService;
using BookResale.Models.Dtos;
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
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<OrderService>();
builder.Services.AddTransient<OrderDto>();
builder.Services.AddScoped<IApprovalStatusService, ApprovalStatusService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IInboxService, InboxService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<BookResale.Admin.Services.UserService.UserService>();


await builder.Build().RunAsync();
