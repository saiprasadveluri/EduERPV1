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
        public async Task<IActionResult> AddSubscription(OrgnizationFeatureSubscriptionDTO inp)
        {
            try
            {
                //Check Org is of same Type (AppModuleType) as that of Feature
                (Guid newSubscriptionId,bool Status) =_business.AddOrganizationFeatureSubscription(inp);
                if(Status)
                    return Ok(new { Status = 1, Data = newSubscriptionId });
               
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data =501, Message = "Error In operation" });
        }
        [HttpGet("{Moduleid}")]
        public async Task<IActionResult> GetFeatures(Guid Moduleid)
        {
            var dto = _business.GetModuleFeatures(Moduleid);
            if(dto.FeatureList!=null && dto.FeatureList.Count>0)
            {
                return Ok(new { Status = 1, Data = dto.FeatureList });
            }
            return BadRequest(new { Status = 0, Data = 501, Message = "Error In operation" });
        }

    }
}
