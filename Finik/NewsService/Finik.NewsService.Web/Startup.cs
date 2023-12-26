using Finik.Data.Repositories;
using Finik.Data;
using Finik.NewsService.Core.Abstractions.Services;
using Finik.NewsService.DbData;
using Finik.NewsService.Infrastructure.Mappers;
using Finik.NewsService.Infrastructure.Services;
using Finik.NewsService.Web.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Finik.NewsService.Infrastructure;

namespace Finik.NewsService.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Configuration["AuthOptions:Issuer"],
                ValidAudience = Configuration["AuthOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(Configuration["AuthOptions:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization();

        services.AddControllers();
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = ApiVersion.Default;
        });

        services.AddScoped<INewsManager, NewsManager>();
        services.AddScoped<INewsDbRepository, NewsEfRepository>();
        services.AddScoped<INewsPublisher, RabbitNewsPublisher>();
        services.AddAutoMapper(typeof(NewsProfile));
        var connectionString = Configuration.GetConnectionString("PostgresFinikNewsDb");
        services.AddDbContext<FinikDbContext>(options => options.UseNpgsql(connectionString));


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.Configure<RabbitMqOptions>(Configuration.GetSection("RabbitMq"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(options => options.MapControllers());
    }
}
