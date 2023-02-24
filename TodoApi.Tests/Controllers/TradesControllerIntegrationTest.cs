using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApi.DTOs;
using Xunit;

namespace TodoApi.Tests.Controllers
{
    public class TradesControllerIntegrationTest
    {
        private TestServer _server;

        public HttpClient Client { get; private set; }

        public TradesControllerIntegrationTest()
        {
            SetUpClient();
        }

        [Fact]
        public async Task PostMethod()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = 100,
                Symbol = "AC",
                Type = "Buy",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var response = await Client.PostAsync(
                "/api/trades",
                new StringContent(JsonSerializer.Serialize(tradeDto), Encoding.UTF8, "application/json")
            );

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task PostMethod_SharesIsLessThan0()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = -1,
                Symbol = "AC",
                Type = "Buy",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var response = await Client.PostAsync(
                "/api/trades",
                new StringContent(JsonSerializer.Serialize(tradeDto), Encoding.UTF8, "application/json")
            );

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task PostMethod_SharesIsGreaterThan100()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = 1012,
                Symbol = "AC",
                Type = "Buy",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var response = await Client.PostAsync(
                "/api/trades",
                new StringContent(JsonSerializer.Serialize(tradeDto), Encoding.UTF8, "application/json")
            );

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task PostMethod_TradeTypeIsNotValid()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = 10,
                Symbol = "AC",
                Type = "xxx",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var response = await Client.PostAsync(
                "/api/trades",
                new StringContent(JsonSerializer.Serialize(tradeDto), Encoding.UTF8, "application/json")
            );

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetMethod()
        {
            //Act
            var response = await Client.GetAsync("/api/trades");

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task HttpPutMethod()
        {
            //Act
            var response = await Client.PutAsync("/api/trades/10", new StringContent(JsonSerializer.Serialize(new TradeDto()), Encoding.UTF8, "application/json"));

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status405MethodNotAllowed);
        }

        [Fact]
        public async Task HttpPatchMethod()
        {
            //Act
            var response = await Client.PatchAsync("/api/trades/10", new StringContent(JsonSerializer.Serialize(new TradeDto()), Encoding.UTF8, "application/json"));

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status405MethodNotAllowed);
        }

        [Fact]
        public async Task HttpDeleteMethod()
        {
            //Act
            var response = await Client.DeleteAsync("/api/trades/10");

            //Assert
            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status405MethodNotAllowed);
        }

        private void SetUpClient()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {

                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }
    }
}
