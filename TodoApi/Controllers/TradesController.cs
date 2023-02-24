using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradesController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        /// <summary>
        /// Get all by query options.
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] int user_id, [FromQuery] string type)
        {
            var result = _tradeService.GetAll(user_id, type);

            if (!result.Success) return BadRequest(result);

            return Ok(result.Result);
        }

        /// <summary>
        /// Get a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _tradeService.GetById(id);

            if (!result.Success) return BadRequest(result);
          
            if (result.Result is null) return NotFound();

            return Ok(result.Result);
        }

        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <returns>A newly created record.</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TradeDto tradeDto)
        {
            var result = await _tradeService.CreateAsync(tradeDto);

            if (!result.Success) return BadRequest(result);

            return Created("/", result.Result);
        }

        /// <summary>
        /// Returns not allowed method (405 code)
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] TradeDto tradeDto)
        {
            Response.Headers.Add("Allow", "GET, POST");
                
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        /// <summary>
        /// Returns not allowed method (405 code)
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TradeDto tradeDto)
        {
            Response.Headers.Add("Allow", "GET, POST");

            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        /// <summary>
        /// Returns not allowed method (405 code)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response.Headers.Add("Allow", "GET, POST");

            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
