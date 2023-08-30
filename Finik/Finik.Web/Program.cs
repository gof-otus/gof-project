using Finik.Core.Abstractions.Services;
using Finik.Data;
using Finik.Data.Repositories;
using Finik.DbData;
using Finik.Infrastructure.Mappers;
using Finik.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INewsManager, NewsManager>();
builder.Services.AddScoped<INewsDbRepository, NewsEfRepository>();
builder.Services.AddScoped<IUsersDbRepository, UsersEfRepository>();
builder.Services.AddAutoMapper(typeof(NewsProfile));
var connectionString = builder.Configuration.GetConnectionString("PostgresFinikDb");
builder.Services.AddDbContext<FinikDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();