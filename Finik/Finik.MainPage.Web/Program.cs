using Microsoft.EntityFrameworkCore;
using Finik.MainPage.Core.Facade;
using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Repositories;
using Finik.MainPage.Core.Services;
using Finik.MainPage.EFData.Repositories;
using Finik.MainPage.EFData;
using Finik.MainPage.Infrastructure.Facade;
using Finik.MainPage.Web.Rabbit;
using Finik.MainPage.Infrastructure.Interfaces;
using Finik.MainPage.Infrastructure.Services;
using Finik.MainPage.Infrastructure;
using Finik.NewsService.Infrastructure.Mappers;



namespace Finik.MainPage.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning();
            builder.Services.AddTransient<IStockManager, StockManager>();
            builder.Services.AddTransient<IStockRepository, StockRepository>();
            builder.Services.AddTransient<IStockFacade, StockFacade>();
            builder.Services.AddTransient<ICompanyFacade, CompanyFacade>();
            builder.Services.AddTransient<ICompanyManager, CompanyManager>();
            builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
            builder.Services.AddTransient<INewsManager, NewsManager>();
            builder.Services.AddTransient<INewsRepository, NewsRepository>();
            builder.Services.AddAutoMapper(typeof(NewsProfile));
            builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
            var stockAndCompanyService = new StockAndCompanyService(builder.Configuration.GetSection("StockAndCompanyService").Value);
            builder.Services.AddTransient<ICompanyService, CompanyService>(o => new CompanyService(stockAndCompanyService));
            builder.Services.AddTransient<IStockService, StockService>(o => new StockService(stockAndCompanyService));
            builder.Services.AddDbContext<MainPageDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FinikMainPage")), ServiceLifetime.Transient, ServiceLifetime.Transient);
            builder.Services.AddHostedService<Listener>();

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
        }
    }
}