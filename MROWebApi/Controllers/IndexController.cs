using Microsoft.AspNetCore.Mvc;

namespace MROWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        #region Get Index
        [HttpGet]
        public string Get()
        { 
            return "API Running ";
        }
        #endregion
    }
}
