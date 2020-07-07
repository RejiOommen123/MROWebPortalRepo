using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using MROWebApi.Services;
using System;
using System.Threading.Tasks;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[APIKeyAuth]
    public class AuthController : ControllerBase
    {
        #region Auth Constructor
        private readonly DBConnectionInfo _info;
        public AuthController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion

        #region Return Admin ID
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAdminUserID(AdminUsers adminUser)
        {
            try
            {
                AdminUsersRepository adminUserRepo = new AdminUsersRepository(_info);
                int nAdminUserID = await adminUserRepo.GetAdminUserID(adminUser.sName,adminUser.sUPN,adminUser.sEmail);
                return Ok(nAdminUserID);
            }
            catch (Exception exp)
            {
                return Content(exp.Message);
            }
        }
        #endregion
    }
}