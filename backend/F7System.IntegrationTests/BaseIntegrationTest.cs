using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoFixture;
using F7System.Api;
using F7System.Api.Infrastructure.Models;
using F7System.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace F7System.IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected IFixture Fixture { get; set; }        
        protected F7DbContext _f7DbContext;
        protected TestServer _server;
        protected HttpClient _client;

        protected BaseIntegrationTest()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            var builder = new DbContextOptionsBuilder<F7DbContext>();

            builder.UseInMemoryDatabase("F7DbContext");

            _f7DbContext = new F7DbContext(builder.Options);
            _f7DbContext.Database.EnsureDeleted();

            var request = new
            {
                Url = "/User/Authenticate",
                Body = new LoginModel()
                {
                    Username = "admin", 
                    Password = "admin"
                }
            };

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            
            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = _server.CreateClient();

            var response = _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body)).Result;
            var jsonTokenResponse = response.Content.ReadAsStringAsync().Result;
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonTokenResponse);

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
        }

        protected async Task<HttpResponseMessage> DoPostRequest(string url, object body)
        {
            
            var request = new
            {
                Url = url,
                Body = body
            };
            
            return await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        }

        protected async Task<HttpResponseMessage> DoGetRequest(string url, object body)
        {
            
            var request = new
            {
                Url = url,
                Body = body
            };
            
            return await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
        }
    }
}