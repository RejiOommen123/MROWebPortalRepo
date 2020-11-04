using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System;
using System.Threading.Tasks;

namespace MROWebApi.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionAuthAttribute : Attribute, IAsyncActionFilter
    {
        //Key from appsettings.json
        private string sRequestorIDHeaderName = "sRequestorID";
        private string sGUIDHeaderName = "sGUID";
        private int nRequestorID = 0;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Before Request
            if (!context.HttpContext.Request.Headers.TryGetValue(sRequestorIDHeaderName, out var sPotentialRequestorIDValue)
                || !context.HttpContext.Request.Headers.TryGetValue(sGUIDHeaderName, out var sPotentialGUIDValue))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var _info = new DBConnectionInfo();
            _info.ConnectionString = configuration.GetConnectionString("myconn");

            bool success = Int32.TryParse(sPotentialRequestorIDValue, out nRequestorID);
            if(success)
            {
                RequestersRepository requestorsFac = new RequestersRepository(_info);
                try
                {
                    string GUIDfromDB = await requestorsFac.SelectGUID(nRequestorID);
                    if (!(!String.IsNullOrEmpty(GUIDfromDB) && ((GUIDfromDB).Equals(sPotentialGUIDValue))))
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        return;
                    }
                }
                catch(Exception ex)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }

            }
            else
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            await next();
        }
    }
}
