using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Domain;
using TodoApi.DTOs;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class SettlementService : ISettlementService
    {
        private readonly ISettlementRepository _settlementRepository;

        public SettlementService(ISettlementRepository settlementRepository)
        {
            _settlementRepository = settlementRepository;
        }

        public ServiceResult<IEnumerable<SettlementDto>> GetSettlementByDates(SettlementQuery query)
        {
            if (DateTime.TryParse(query.StartDate, out DateTime startDate) && DateTime.TryParse(query.EndDate, out DateTime endDate))
            {
                var settlements = _settlementRepository.GetSettlementByDates(startDate, endDate, query.SettlementLocationID);

                if (!settlements.Any())
                {
                    return new ServiceResult<IEnumerable<SettlementDto>>()
                    {
                        Success = false,
                        Message = "Dates Not Found"
                    };
                }

                var result = settlements.Select(s => new SettlementDto {
                    SettlementLocationName = s.SettlementLocationName,
                    DateOfService = s.DateOfService,
                    PricePerMWh = s.PricePerMWh,
                    VolumeMWh = s.VolumeMWh
                }).ToList();

                return new ServiceResult<IEnumerable<SettlementDto>>() {
                    Success = true,
                    Result = result
                };
            }

            return new ServiceResult<IEnumerable<SettlementDto>>()
            {
                Success = false,
                Message = "Please provide valid dates. Format Allowed: YYYY-MM-DDDD ex: 2023-02-24"
            };
        }

        public ServiceResult<IEnumerable<SettlementGroupByYearDto>> GroupByYear(string settlementLocationName)
        {
            var result = _settlementRepository.GroupByYear(settlementLocationName)
                .Select(g => new SettlementGroupByYearDto
                {
                    SettlemenLocation = g.Key.SettlementLocationName,
                    YearDate = new DateTime(g.Key.Year, g.Min(s => s.DateOfService).Month, 1),
                    AveragePrice = g.Average(s => s.PricePerMWh),
                    AverageVolume = g.Average(s => s.VolumeMWh),
                    AverageTotalDollars = g.Average(s => s.PricePerMWh * s.VolumeMWh),
                    MinPrice = g.Min(s => s.PricePerMWh),
                    MaxPrice = g.Max(s => s.PricePerMWh)
                }).AsEnumerable();


            return new ServiceResult<IEnumerable<SettlementGroupByYearDto>>()
            {
                Success = true,
                Result = result
            };
        }
    }
}
