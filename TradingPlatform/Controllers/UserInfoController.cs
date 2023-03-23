using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingPlatform.Data;
using TradingPlatform.Model;

namespace TradingPlatform.Controllers
{
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        private readonly DbHelper _db;
        public UserInfoController(DataContext dataContext)
        {
            _db = new DbHelper(dataContext);
        }

        [HttpGet]
        [Route("api/[controller]/all")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<UserInfoModel> data = _db.GetUserInfo();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
                    UserInfoModel data = _db.GetUserInfoById(currentUserID);
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
        public IActionResult Post([FromBody] UserInfoModel model)
        {
            try
            {
                if (model.passcode.Contains("500"))
                {
                    model.type = "admin";
                }
                else
                {
                    model.type = "user";
                }

                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine("update currentUserID : ", currentUserID);

                if (currentUserID != null)
                {
                    UserInfoModel data = _db.GetUserInfoById(currentUserID);
                    Console.WriteLine("update data : ", data.ToString());

                    if (data.email.Equals(""))
                    {
                        _db.SaveUserInfo(model, currentUserID);
                    }
                    
                    else
                    {                       
                        model.cash_balance = data.cash_balance;
                        _db.UpdateUserInfo(model, currentUserID);
                    }

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
