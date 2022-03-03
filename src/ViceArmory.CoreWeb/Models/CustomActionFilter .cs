using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Authenticate;

namespace ViceArmory.CoreWeb.Models
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.ExceptionHandled = true;
                context.Result = new RedirectToRouteResult
                   (
                   new RouteValueDictionary(new
                   {
                       action = "index",
                       controller = "AdminLogin"
                   }));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var objSession = context.HttpContext.Session.GetString("UserInfo");
            if (string.IsNullOrEmpty(objSession))
            {
                //var authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(context.HttpContext.Session.ToString());
                context.Result = new RedirectToRouteResult
            (
            new RouteValueDictionary(new
            {
                action = "index",
                controller = "AdminLogin"
            }));

            }

        }

    }
}

