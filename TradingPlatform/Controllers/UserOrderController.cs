using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Data;
using TradingPlatform.Model;

namespace TradingPlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class UserOrderController : ControllerBase
    {
        private readonly DbHelper _db;
        public UserOrderController(DataContext dataContext)
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
                    List<UserOrderModel> data = _db.GetUserOrderById(currentUserID);
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
        public IActionResult Post([FromBody] UserOrderModel model)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (currentUserID != null)
                {
                    _db.SaveUserOrder(model, currentUserID);
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

        [HttpPost]
        [Route("api/[controller]/cancel")]
        public IActionResult Post([FromBody] CancelOrderModel model)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (currentUserID != null)
                {
                    _db.CancelUserOrder(model, currentUserID);
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
