using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly DbSet<Settlement> _settlements;

        public SettlementRepository(TodoContext todoContext)
        {
            _settlements = todoContext.Settlements;
        }

        public IQueryable<Settlement> GetSettlementByDates(DateTime startDate, DateTime endDate, int? settlementLocationID)
        {
            return _settlements
                            .Where(s => !settlementLocationID.HasValue || s.SettlementLocationID == settlementLocationID)
                            .Where(s => s.DateOfService >= startDate && s.DateOfService <= endDate);
        }

        public IQueryable<IGrouping<SettlementGroupByYear, Settlement>> GroupByYear(string settlementLocationName)
        {
            return _settlements.Where(s => string.IsNullOrEmpty(settlementLocationName) || s.SettlementLocationName == settlementLocationName)
                           .GroupBy(s => new SettlementGroupByYear() { SettlementLocationName = s.SettlementLocationName, Year = s.DateOfService.Year });
        }
    }
}
