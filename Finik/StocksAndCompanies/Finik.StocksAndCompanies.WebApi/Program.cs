using Microsoft.EntityFrameworkCore;
using Finik.StocksAndCompanies.EfData;
using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Services;
using Finik.StockAndCompany.Core.Repositories;
using Finik.StocksAndCompanies.EfData.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();
builder.Services.AddScoped<IStockManager, StockManager>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICompanyManager, CompanyManager>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddDbContext<StockAndCompaniesDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FinikSecurities")));
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