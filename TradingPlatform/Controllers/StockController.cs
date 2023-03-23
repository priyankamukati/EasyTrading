using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Data;
using TradingPlatform.Model;

namespace TradingPlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly DbHelper _db;
        public StockController(DataContext dataContext)
        {
            _db = new DbHelper(dataContext);
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<StockModel> data = _db.GetStocks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                StockModel data = _db.GetStockById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post([FromBody] StockModel model)
        {
            try
            {
                _db.SaveStock(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
