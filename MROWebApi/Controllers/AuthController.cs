using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeFirstMigration.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MROWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        //    [HttpPost]
        //    [AllowAnonymous]
        //    [Route("[action]")]
        //    public async Task<ActionResult<AdminUser>> SignUp([FromBody]AdminUser user)
        //    {
        //        AdminUser dbuser = _context.AdminUser.FirstOrDefault(u => u.Email == user.Email);
        //        if (dbuser == null)
        //        {
        //            _context.AdminUser.Add(user);
        //            await _context.SaveChangesAsync();
        //            return Ok("success");
        //        }
        //        return NotFound();
        //    }

        //    [HttpPost]
        //    [AllowAnonymous]
        //    [Route("[action]")]
        //    public async Task<ActionResult<AdminUser>> SignIn([FromBody]AdminUser user)
        //    {
        //        AdminUser dbuser = _context.AdminUser.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        //        if (dbuser != null)
        //        {
        //            return Ok("success");
        //        }
        //        return NotFound();
        //    }
        //}
    }
}