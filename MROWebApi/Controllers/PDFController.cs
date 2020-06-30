using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MRODBL.Entities;


namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PDFController : ControllerBase
    {
        #region PDF Constructor
        private readonly DBConnectionInfo _info;
        private readonly ILogger<PDFController> _logger;
        public PDFController(ILogger<PDFController> logger, DBConnectionInfo info)
        {
            _logger = logger;
            _info = info;
        }
        #endregion
    }
}
