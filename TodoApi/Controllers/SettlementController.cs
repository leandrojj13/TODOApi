using Microsoft.AspNetCore.Mvc;
using TodoApi.Domain;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementService _settlementService;

        public SettlementController(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }

        /// <summary>
        /// Returns list of records
        /// </summary>
        /// <param name="SettlementLocationID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>List of Settlement.</returns>
        [HttpGet("api/ret_settlement_dates")]
        public IActionResult GetSettlementByDates([FromQuery] SettlementQuery query)
        {
            var result = _settlementService.GetSettlementByDates(query);

            if (!result.Success) return BadRequest(result);

            return Ok(result.Result);
        }

        /// <summary>
        /// Returns aggregate Settlement list grouped by year
        /// </summary>
        /// <param name="settlementLocationName"></param>
        /// <returns>List of Settlement.</returns
        [HttpGet("api/agg_monthly")]
        public IActionResult GetSettlementAggMonthly([FromQuery] string settlementLocationName)
        {
            var result = _settlementService.GroupByYear(settlementLocationName);

            if (!result.Success) return BadRequest(result);

            return Ok(result.Result);
        }
    }
}
