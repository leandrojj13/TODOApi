using System.Collections.Generic;
using TodoApi.Domain;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public interface ISettlementService
    {
        ServiceResult<IEnumerable<SettlementDto>> GetSettlementByDates(SettlementQuery query);

        ServiceResult<IEnumerable<SettlementGroupByYearDto>> GroupByYear(string settlementLocationName);
    }
}
