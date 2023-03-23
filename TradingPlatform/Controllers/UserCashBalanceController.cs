using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Data;
using TradingPlatform.Model;

namespace TradingPlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class UserCashBalanceController : ControllerBase
    {
        private readonly DbHelper _db;
        public UserCashBalanceController(DataContext dataContext)
        {
            _db = new DbHelper(dataContext);
        }


        [HttpGet]
        [Route("api/[controller]/")]
        public IActionResult Get()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (currentUserID != null)
                {
                    UserInfoModel data = _db.GetUserCashBalanceById(currentUserID);
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

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post([FromBody] UpdateCashBalanceRequest model)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (currentUserID != null)
                {
                    _db.SaveUserCashBalance(model, currentUserID);
                    return Ok(model);
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
