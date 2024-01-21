using Microsoft.EntityFrameworkCore;
using Finik.StocksAndCompanies.EfData;
using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Services;
using Finik.StockAndCompany.Core.Repositories;
using Finik.StocksAndCompanies.EfData.Repositories;
using Finik.StocksAndCompanies.WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();
builder.Services.AddTransient<IStockManager, StockManager>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<ICompanyManager, CompanyManager>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddDbContext<StockAndCompaniesDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FinikSecurities")), ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddHostedService<Grabber>();
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