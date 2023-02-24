using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ISettlementRepository
    {
        IQueryable<Settlement> GetSettlementByDates(DateTime startDate, DateTime endDate, int? settlementLocationID);

        IQueryable<IGrouping<SettlementGroupByYear, Settlement>> GroupByYear(string settlementLocationName);
    }
}
