using Azure.Core;
using EduERPApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace EduERPApi.Infra
{
    public class OrganizationValueReaderFilter:IActionFilter
    {
        string[] BypassedControllers = { "Organization", "Account" };
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string CurControllerName = context.HttpContext.GetRouteValue("controller").ToString().ToUpper();
            var found = (from str in BypassedControllers
                         where str.ToUpper() == CurControllerName.ToUpper()
                         select str).FirstOrDefault();

                        

            if (found!=null)
            {
                return;
            }
            context.HttpContext.Session.Remove("UserId");
            context.HttpContext.Session.Remove("OrgId");
            
            var (OrgId, UserId) = ReadInfoIdFromJWTToken(context.HttpContext.Request);
            if (OrgId != null && UserId != null)
            {
                context.HttpContext.Session.SetString("UserID", UserId);
                context.HttpContext.Session.SetString("OrgId", OrgId);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Error In Getting UserID and OrgID"
                };

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Session.Remove("UserId");
            context.HttpContext.Session.Remove("OrgId");
        }

        private (string?,string?) ReadInfoIdFromJWTToken(HttpRequest request)
        {
            try
            {
                StringValues AuthString;
                bool AuthStringPresent = request.Headers.TryGetValue("Authorization", out AuthString);
                if (!AuthStringPresent)
                {
                    return (null,null);
                }
                string[] ValueArray = AuthString.ToArray();
                if (ValueArray.Length == 0)
                {
                    return (null,null);
                }
                string HeaderValue = ValueArray[0];
                string[] AuthHeaderValues = HeaderValue.Split(' ');
                var stream = AuthHeaderValues[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var jtiOrgId = tokenS.Claims.First(claim => claim.Type == "OrgId").Value;
                var jtiUserId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
                return (jtiOrgId, jtiUserId);
                
            }
            catch(Exception ex)
            {

            }
            return (null, null);
        }
    }
}
