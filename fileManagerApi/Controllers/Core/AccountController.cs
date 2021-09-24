using Microsoft.AspNetCore.Mvc;

namespace fileManagerApi.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("serverStatusAlive")]
        public bool serverStatusAlive()
        {
            return true;
        }
    }
}
