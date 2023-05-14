using API;
using DAL;

var builder = WebApplication.CreateBuilder(args);


using (var context = new ViolationContext())
{
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    context.SaveChanges();
}

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });