using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
  
    public class BaseController : ControllerBase
    {
        protected int UserId
        {
            get
            {
                int.TryParse(HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="userId")?.Value.ToString(), out int userId);
                return userId;
            }
        }
    }
}
