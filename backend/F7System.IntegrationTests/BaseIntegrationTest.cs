using System.Linq;
using System.Net.Http;
using AutoFixture;
using F7System.Api;
using F7System.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

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


            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            
            _server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = _server.CreateClient();

        }
    }
}