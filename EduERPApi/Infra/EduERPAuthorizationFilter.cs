using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduERPApi.Infra
{
    public class EduERPAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        string[] BypassedControllers = { "Organization", "Account", "OrgnizationFeatureSubscription", "ModuleFeature" };
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string currentController=context.HttpContext.GetRouteValue("controller").ToString().ToUpper();
            if (currentController == "ACCOUNT")
                return;
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var found = (from str in BypassedControllers
                         where str.ToUpper() == currentController.ToUpper()
                         select str).FirstOrDefault();
            if (found != null)
            {
                return;
            }
            context.HttpContext.Session.Remove("UserId");
            context.HttpContext.Session.Remove("OrgId");

            var authStringVals = context.HttpContext.Request.Headers["Authorization"];
            if(authStringVals.Count()!=1)
            {
                context.Result = new UnauthorizedResult();
            }

            string authzString = authStringVals[0];
            var (OrgId, UserId,IsSysAdmin,Status) = ReadInfoIdFromJWTToken(authzString);
            if(IsSysAdmin==1)
            {
                return;
            }
            if (Status==true)
            {
                context.HttpContext.Session.SetString("UserID", UserId);
                context.HttpContext.Session.SetString("OrgId", OrgId);
                return;
            }
                context.Result = new UnauthorizedResult();
        }

        

        private (string?, string?,int,bool) ReadInfoIdFromJWTToken(string JwtTokenString)
        {
            try
            {
               
                /*string[] ValueArray = AuthString.ToArray();
                if (ValueArray.Length == 0)
                {
                    return (null, null);
                }
                string HeaderValue = ValueArray[0];*/

                string[] AuthHeaderValues = JwtTokenString.Split(' ');
                if(AuthHeaderValues.Length!=2)
                {
                   return (null, null,0,false);
                }
                var JwtTokenPart = AuthHeaderValues[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(JwtTokenPart);
                var tokenS = jsonToken as JwtSecurityToken;
                var isSysAdminClaim = tokenS.Claims.FirstOrDefault(claim => claim.Type == "IsSysAdmin");
                if (isSysAdminClaim != null)
                {
                    if(isSysAdminClaim.Value=="1")
                    {
                        return (null, null, 1, true);
                    }
                }
                var jtiOrgId = tokenS.Claims.First(claim => claim.Type == "OrgId").Value;
                var jtiUserId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
                return (jtiOrgId, jtiUserId,0,true);

            }
            catch (Exception ex)
            {

            }
            return (null, null,0,false);
        }
    }
}
