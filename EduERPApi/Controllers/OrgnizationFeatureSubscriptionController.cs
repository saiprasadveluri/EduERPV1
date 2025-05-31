using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgnizationFeatureSubscriptionController : ControllerBase
    {
        Business _business;
        public OrgnizationFeatureSubscriptionController(Business business)
        {
            _business = business;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscription(List<OrgnizationFeatureSubscriptionDTO> inp)
        {
            try
            {
                //Check Org is of same Type (AppModuleType) as that of Feature
               bool Status =_business.AddOrganizationFeatureSubscription(inp);
                if(Status)
                    return Ok(new { Status = 1, Data = "Success" });
               
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data =501, Message = "Error In operation" });
        }
        
        [HttpGet("{OrgId}")]
        public async Task<IActionResult> GetAllFeatureByOrgId(Guid OrgId)
        {
            var Res = _business.GetAllOrgnizationFeatureSubscriptions(OrgId);
            return Ok(new { Status = 1, Data = Res });
        }
        [HttpDelete("{SubIdIdArrayString}")]
        public async Task<IActionResult> RemoveFeatureToOrg(string SubIdIdArrayString)
        {
           bool Status= _business.RemoveFeatureToOrg(SubIdIdArrayString);
            if(Status)
            {
                return Ok(new { Status = 1, Data = "Success" });
            }
            return BadRequest(new { Status = 0, Data = 502, Message = "Error In operation" });
        }

    }
}
