using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgUserFeatureRoleMapController : ControllerBase
    {
        
        private Business _businessObj;
        public OrgUserFeatureRoleMapController(Business businessObj)
        {
            _businessObj = businessObj;
        }

        [HttpPost]
        public async Task<IActionResult> AddMap(AppUserFeatureRoleMapDTO inp)
        {
            
            try
            {
                (Guid Id, bool Status) = _businessObj.AddOrgUserFeatureRoleMap(inp);
                if (Status)
                    return Ok(new { Status = 1, Data = Id });
            }
            catch(Exception ex)
            {
                
            }

            return BadRequest(new { Status = 0, Data = "Error In Operation" });
        }        
    }
}
