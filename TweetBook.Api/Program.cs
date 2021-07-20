using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TweetBook.Data;

namespace TweetBook.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync("Admin"))
                {
                    var admin = new IdentityRole("Admin");
                    await roleManager.CreateAsync(admin);
                }

                if (!await roleManager.RoleExistsAsync("Poster"))
                {
                    var poster = new IdentityRole("Poster");
                    await roleManager.CreateAsync(poster);
                }
            }

                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
