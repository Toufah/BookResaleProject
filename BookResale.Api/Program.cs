using BookResale.Api.Data;
using BookResale.Api.Repositories;
using BookResale.Api.Repositories.Contracts;
using BookResale.Api.Services;
using BookResale.Api.Services.BookServices;
using BookResale.Api.Services.OrderService;
using BookResale.Api.Services.PaymentServices;
using BookResale.Api.Services.SellerBankAccountInfoService;
using BookResale.Api.Services.StatsService;
using BookResale.Api.Services.TrackingService;
using BookResale.Api.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<BookResaleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookResaleConnection")));

builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<CartItemsRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<FilterRepository>();
builder.Services.AddScoped<IFilterRepository, FilterRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApprovalsRepository, ApprovalsRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISellerBankAccountInfoService, SellersBankAccountInfoService>();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy =>
    Policy.WithOrigins("http://localhost:7270", "https://localhost:7270", "http://localhost:7187", "https://localhost:7187")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
