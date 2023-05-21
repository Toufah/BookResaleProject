using BookResale.Api.Data;
using BookResale.Api.Repositories;
using BookResale.Api.Repositories.Contracts;
using BookResale.Api.Services;
using BookResale.Api.Services.PaymentServices;
using BookResale.Api.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy =>
    Policy.WithOrigins("http://localhost:7270", "https://localhost:7270").AllowAnyMethod().WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
