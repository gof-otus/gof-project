using Finik.Core.Abstractions.Services;
using Finik.Data;
using Finik.Data.Repositories;
using Finik.DbData;
using Finik.Infrastructure.Mappers;
using Finik.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Finik", Version = "v1" });
});

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
