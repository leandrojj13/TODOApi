using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Domain;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Services
{
    public class SettlementServiceTest
    {
        private readonly SettlementService _sut;

        private readonly List<Settlement> _settlements = new List<Settlement>()
        {
            new Settlement(),
            new Settlement(),
            new Settlement(),
            new Settlement(),
            new Settlement()
        };

        private readonly Mock<ISettlementRepository> _settlementRepository;


        public SettlementServiceTest()
        {
            _settlementRepository = new Mock<ISettlementRepository>();

            _settlementRepository
                .Setup(f => f.GetSettlementByDates(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int?>()))
                .Returns(_settlements.AsQueryable());

            _sut = new SettlementService(_settlementRepository.Object);
        }

        [Fact]
        public void GetSettlementByDates_HasAllSettlementParameters_ReturnsList()
        {
            //Arrange
            var query = new SettlementQuery()
            {
                StartDate = "2022-02-02",
                EndDate = "2024-02-02",
                SettlementLocationID = 1
            };

            //Act
            var result = _sut.GetSettlementByDates(query);

            //Assert
            result.Success.Should().BeTrue();
            result.Result.Should().HaveCount(_settlements.Count());
        }

        [Fact]
        public void GetSettlementByDates_HasInvalidStartDate_ReturnsResultAsNotSuccess()
        {
            //Arrange
            var query = new SettlementQuery()
            {
                StartDate = "1111",
                EndDate = "2024-02-02",
                SettlementLocationID = 1
            };

            //Act
            var result = _sut.GetSettlementByDates(query);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Please provide valid dates. Format Allowed: YYYY-MM-DDDD ex: 2023-02-24");
        }

        [Fact]
        public void GetSettlementByDates_HasInvalidEndDate_ReturnsResultAsNotSuccess()
        {
            //Arrange
            var query = new SettlementQuery()
            {
                StartDate = "2022-02-02",
                EndDate = "AAA",
                SettlementLocationID = 1
            };

            //Act
            var result = _sut.GetSettlementByDates(query);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Please provide valid dates. Format Allowed: YYYY-MM-DDDD ex: 2023-02-24");
        }

        [Fact]
        public void GetSettlementByDates_HasNotRecords_ReturnsResultAsNotSuccess()
        {
            //Arrange
            _settlementRepository
                .Setup(f => f.GetSettlementByDates(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int?>()))
                .Returns(new List<Settlement>().AsQueryable());

            var query = new SettlementQuery()
            {
                StartDate = "2022-02-02",
                EndDate = "2024-02-02",
                SettlementLocationID = 1
            };

            //Act
            var result = _sut.GetSettlementByDates(query);

            //Assert
            result.Success.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Messages.Should().Contain("Dates Not Found");
        }
    }
}
