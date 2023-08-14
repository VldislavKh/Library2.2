using Domain.Infrastructure;
using Library2._2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.IntegrationTests
{
    public class TestingWebAppFactory : WebApplicationFactory<Program>
    {
        private readonly string dbConnectionString = "Host=localhost;Port=5436;Database=test-library-db;" +
                "Username=dbuser;Password=testdbpassword"; 

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseNpgsql(dbConnectionString);
                });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            });
        }
    }
}
