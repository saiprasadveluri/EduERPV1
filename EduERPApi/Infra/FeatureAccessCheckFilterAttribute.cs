using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Text.Json;

namespace EduERPApi.Infra
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FeatureAccessCheckFilterAttribute : Attribute, IActionFilter
    {
        Guid ReqAppRoleId;
        Business _Business;
        public FeatureAccessCheckFilterAttribute(string reqAppRoleId)
        {
            ReqAppRoleId = Guid.Parse(reqAppRoleId);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            if (context.HttpContext.Session.GetString("IsSysAdmin") != null)
            {
                return;
            }

            if (context.HttpContext.Session.GetString("IsOrgAdmin") != null)
            {
                
                    List<Guid> SubscribedFeatureList = JsonSerializer
                        .Deserialize<List<Guid>>(context.HttpContext.Session.GetString("AllowedfeaturesJson"));
                    if (SubscribedFeatureList.Contains(ReqAppRoleId))
                    {
                        return;
                    }
                    else
                    {
                        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                    }
                }
            
                if (context.HttpContext.Session.GetString("OrgId") != null && context.HttpContext.Session.GetString("UserId") != null)
                {                    
                    List<Guid> SubscribedFeatureList = JsonSerializer
                        .Deserialize<List<Guid>>(context.HttpContext.Session.GetString("AllowedfeaturesJson"));
                    if (SubscribedFeatureList.Contains(ReqAppRoleId))
                    {
                        return;
                    }
                }
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}
