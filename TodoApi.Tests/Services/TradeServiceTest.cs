using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TodoApi.DTOs;
using TodoApi.Repositories;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Services
{
    public class TradeServiceTest
    {
        private readonly TradeService _sut;
       
        public TradeServiceTest()
        {
            var tradeRepository = new Mock<ITradeRepository>();
     
            _sut = new TradeService(tradeRepository.Object);
        }

        [Fact]
        public async Task CreateAsync_SharesIsLessThan0_ReturnsResultAsNotSuccess()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = -5,
                Symbol = "AC",
                Type = "Buy",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var result = await _sut.CreateAsync(tradeDto);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Shares values must be within [1, 100]");
        }

        [Fact]
        public async Task CreateAsync_SharesIsGreaterThan100_ReturnsResultAsNotSuccess()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = 101,
                Symbol = "AC",
                Type = "Buy",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var result = await _sut.CreateAsync(tradeDto);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Shares values must be within [1, 100]");
        }

        [Fact]
        public async Task CreateAsync_TradeTypeIsNotValid_ReturnsResultAsNotSuccess()
        {
            //Arrange
            var tradeDto = new TradeDto()
            {
                Price = 162,
                Shares = 100,
                Symbol = "AC",
                Type = "XXX",
                UserId = 1,
                Timestamp = 1591514264000
            };

            //Act
            var result = await _sut.CreateAsync(tradeDto);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Type should be either 'Sell' or 'Buy'");
        }
    }
}
