using Domain.Entities;
using FluentAssertions;
using Library2._2.Commands.AuthorCommands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Library.IntegrationTests
{
    public class AuthorControllerIntegrationTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;

        public AuthorControllerIntegrationTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void AddAuthorTest()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/Author/AddAuthor");

            //var formModel = new Dictionary<string, string>
            //{
            //    { "name", "Èìÿ" },
            //    { "birthDate", "1997-08-10" },
            //    { "deathDate", "2023-08-10"}
            //};

            //postRequest.Content = new FormUrlEncodedContent(formModel);

            var sc = new StringContent(JsonSerializer.Serialize(new AddAuthorCommand
            {
                Name = "name",
                BirthDate = DateOnly.Parse("1997-08-10"),
                DeathDate = DateOnly.Parse("2023-08-10")
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Author/AddAuthor", sc);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsAsync<int>();

            responseString.Should().BeGreaterThan(0);
        }
    }
}