using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementController : ControllerBase
    {
        private readonly TodoContext _todoContext;

        public SettlementController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _todoContext.Database.EnsureCreated();

            var settlements = _todoContext.Settlements.ToList();

            return Ok(settlements);
        }
    }
}
