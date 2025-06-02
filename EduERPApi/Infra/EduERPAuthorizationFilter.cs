using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using EduERPApi.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
namespace EduERPApi.Infra
{
    public class EduERPAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        string[] BypassedControllers = { "Organization", "Account", "OrgnizationFeatureSubscription", "ModuleFeature" };
        const int SYS_ADMIN = 1, ORG_ADMIN = 1;
        Business _busines;
        public EduERPAuthorizationFilter(Business business)
        {
            _busines = business;
        }
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
            var (OrgId, UserId,IsSysAdmin,IsOrgAdmin,Status) = ReadInfoIdFromJWTToken(authzString);
            if(Status==true)
            { 
                if(IsSysAdmin==1)
                {
                    context.HttpContext.Session.SetString("IsSysAdmin", "1");
                    return;
                }
                if(IsOrgAdmin==1)
                {
                    context.HttpContext.Session.SetString("IsOrgAdmin", "1");                
                }
                context.HttpContext.Session.SetString("UserID", UserId);
                context.HttpContext.Session.SetString("OrgId", OrgId);
                SetFetuareRoleAccessSession(context.HttpContext,Guid.Parse(OrgId), Guid.Parse(UserId), IsOrgAdmin);

                return;
            }
                context.Result = new UnauthorizedResult();
        }

        private void SetFetuareRoleAccessSession(HttpContext httpContext,Guid OrgId, Guid UserId, int IsOrgAdmin)
        {
            if(IsOrgAdmin==1)
            {
                var OrgSubDtoList= _busines.GetAllOrgnizationFeatureSubscriptions(OrgId);
                var AllowdFeatreIds = OrgSubDtoList.Select(of => of.FeatureId).ToList();
                string AllowedfeaturesJson = JsonSerializer.Serialize(AllowdFeatreIds);
                httpContext.Session.SetString("AllowedfeaturesJson", AllowedfeaturesJson);
            }
            else
            {
                var AllowdFeatreRoleIds= _busines.GetAllUserAllowedFeatures(OrgId, UserId);
                string AllowedfeaturesJson = JsonSerializer.Serialize(AllowdFeatreRoleIds);
                httpContext.Session.SetString("AllowedfeaturesJson", AllowedfeaturesJson);
            }
        }

        private (string?, string?,int,int,bool) ReadInfoIdFromJWTToken(string JwtTokenString)
        {
            try
            {
               
               

                string[] AuthHeaderValues = JwtTokenString.Split(' ');
                if(AuthHeaderValues.Length!=2)
                {
                   return (null, null,0,0,false);
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
                        return (null, null, SYS_ADMIN,0,true);
                    }
                }
                var isOrgAdminClaim = tokenS.Claims.FirstOrDefault(claim => claim.Type == "IsOrgAdmin");
                if (isOrgAdminClaim != null)
                {
                    if (isOrgAdminClaim.Value == "1")
                    {
                        var OrgAdminOrgId = tokenS.Claims.First(claim => claim.Type == "OrgId").Value;
                        var OrgAdminUserId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
                        return (OrgAdminOrgId, OrgAdminUserId, 0,ORG_ADMIN, true);
                    }
                }
                var jtiOrgId = tokenS.Claims.First(claim => claim.Type == "OrgId").Value;
                var jtiUserId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
                return (jtiOrgId, jtiUserId,0,0,true);

            }
            catch (Exception ex)
            {

            }
            return (null, null,0,0,false);
        }
    }
}
