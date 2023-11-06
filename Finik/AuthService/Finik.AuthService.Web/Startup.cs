using Asp.Versioning;
using Finik.AuthService.Core;
using Finik.AuthService.DataAccess;
using Finik.AuthService.EF;
using Finik.AuthService.EF.Repositories;
using Finik.AuthService.Services;
using Finik.AuthService.Services.Profiles;
using Finik.AuthService.Web.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace Finik.AuthService.Web;

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

        services.AddScoped<IUserRepository, UsersEfRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IAuthManager, JwtTokenManager>();
        services.AddSingleton<IPasswordManager, SaltedPasswordManager>();
        services.AddAutoMapper(typeof(UserProfile));
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("PostgresFinikDb"));
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration["Cache:Host"];
            options.InstanceName = Configuration["Cache:Name"];
        });

        services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
    }

    public void Configure(IApplicationBuilder app , IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
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
