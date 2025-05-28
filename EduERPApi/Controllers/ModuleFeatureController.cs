using EduERPApi.BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleFeatureController : ControllerBase
    {
        Business _business;
        public ModuleFeatureController(Business business)
        {
            _business = business;
        }

        [HttpGet("Module/{ModuleiId}")]
        public IActionResult GetAllModuleFeatures(Guid ModuleiId)
        {
            var Res = _business.GetAllModuleFeatures(ModuleiId);
            return Ok(new { Status = 1, Data = Res });
        }
        
    }
}
