using Finik.AuthService.Web;
using Microsoft.AspNetCore;

CreateWebHostBuilder(args).Build().Run();
static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
