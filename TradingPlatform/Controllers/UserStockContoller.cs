using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Data;
using TradingPlatform.Model;

namespace TradingPlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class UserStockController : ControllerBase
    {
        private readonly DbHelper _db;
        public UserStockController(DataContext dataContext)
        {
            _db = new DbHelper(dataContext);
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (currentUserID != null)
                {
                    List<UserStockModel> data = _db.GetUserStockById(currentUserID);
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}

