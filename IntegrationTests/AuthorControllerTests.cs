using Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestFixture]
    public class AuthorControllerTests
    {
        [Test]
        public async Task AddAuthorTest()
        {
            // Arrange

            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var contextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationContext>));

                    services.Remove(contextDescriptor);

                    services.AddDbContext<ApplicationContext>(options =>
                    {
                        options.UseInMemoryDataBase();
                    });
                });
            });

            // Act

            // Assert
        }
    }
}
