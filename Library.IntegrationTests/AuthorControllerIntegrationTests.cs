using Domain.Entities;
using Domain.Infrastructure;
using FluentAssertions;
using Library2._2.Commands.AuthorCommands;
using Library2._2.Commands.BookCommands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.EventHubs.Processor;
using System.Data.Common;
using Npgsql;

namespace Library.IntegrationTests
{
    [Collection("Test Collection")]
    public class AuthorControllerIntegrationTests : IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly IServiceScope _scope;
        private Respawner _respawner;

        private ApplicationContext context;
        private readonly string dbConnectionString = "Host=localhost;Port=5436;Database=test-library-db;" +
                "Username=dbuser;Password=testdbpassword";
        public AuthorControllerIntegrationTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _scope = factory.Services.CreateScope();
            context = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        }

        public async Task InitializeAsync()
        {
            var dbConnection = new NpgsqlConnection(dbConnectionString);
            await dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres
            }); 

            await _respawner.ResetAsync(dbConnection);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }


        [Fact]
        public async void Add_ShouldReturnIdOfAddedAuthor()
        {
            // Arrange
            var sc = new StringContent(JsonSerializer.Serialize(new AddAuthorCommand
            {
                Name = "name",
                BirthDate = DateOnly.Parse("1997-08-10"),
                DeathDate = DateOnly.Parse("2023-08-10")
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Author/AddAuthor", sc);
            response.EnsureSuccessStatusCode();
            // Act
            var responseString = await response.Content.ReadAsAsync<int>();

            // Assert
            responseString.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void Delete_ShouldReturnNoContent()
        {
            // Arrange
            context = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            SetTestData_ThreeAuthorsAndFiveBooks(context);

            // Act
            var response = await _client.DeleteAsync("api/Author/DeleteAuthor/1");
            response.EnsureSuccessStatusCode();
            
            //Assert
            context.Authors.FirstOrDefault(x => x.Id == 1).Should().BeNull();
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfAllAuthors()
        {
            // Arrange
            context = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            SetTestData_ThreeAuthorsAndFiveBooks(context);

            // Act
            var response = await _client.GetAsync("api/Author/GetAllAuthors");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<List<Author>>();

            // Assert
            result.Should().HaveCount(context.Authors.Count());
        }

        [Fact]
        public async void GetBooks_ShouldReturnListOfAuthorsBooks()
        {
            // Arrange
            context = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            SetTestData_ThreeAuthorsAndFiveBooks(context);

            // Asct
            var response = await _client.GetAsync("api/Author/GetAuthorsBooks/2");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<List<Book>>();

            // Assert
            result.Should().HaveCount(context.Books.Count(b => b.AuthorId == 2));
        }

        [Fact]
        public async void GetMaxBooksAuthors_ShouldReturnAuthorsWithMaxAmountOfBooks()
        {
            // Arrange
            context = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            SetTestData_ThreeAuthorsAndFiveBooks(context);

            // Act
            var response = await _client.GetAsync("api/Author/GetMaxBooksAuthors");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<List<Author>>();

            // Assert
            result.Should().HaveCount(context.Authors.Count(a => a.Books.Count == context.Authors.Max(a => a.Books.Count)));
        }

        private void SetTestData_ThreeAuthorsAndFiveBooks(ApplicationContext context)
        {
            //posting authors

            var author1 = new Author
            {
                Name = "AAuthor",
                BirthDate = DateOnly.Parse("1997-08-10"),
                DeathDate = DateOnly.Parse("2023-08-10")
            };

            //context.Add(author1);

            var author2 = new Author
            {
                Name = "CAuthor",
                BirthDate = DateOnly.Parse("1990-08-10"),
                DeathDate = DateOnly.Parse("2020-08-10")
            };

            var author3 = new Author
            {
                Name = "BAuthor",
                BirthDate = DateOnly.Parse("1997-08-10"),
                DeathDate = DateOnly.Parse("2023-08-10")
            };


            //posting books
            context.Add(new Book
            {
                Title = "Title1",
                Year = 1999,
                Genre = "Genre1",
                Author = author1,
            });


            context.Add(new Book
            {
                Title = "Title2",
                Year = 2000,
                Genre = "Genre2",
                Author = author2,
            });

            context.Add(new Book
            {
                Title = "Title3",
                Year = 2001,
                Genre = "Genre3",
                Author = author2,
            });

            context.Add(new Book
            {
                Title = "Title4",
                Year = 2002,
                Genre = "Genre4",
                Author = author3,
            });


            context.Add(new Book
            {
                Title = "Title5",
                Year = 2003,
                Genre = "Genre5",
                Author = author3,
            });

            context.SaveChanges();
        }
    }
}