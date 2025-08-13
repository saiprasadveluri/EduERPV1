using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationModuleController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;
        public ApplicationModuleController(IConfiguration cfg, Business businessObj)
        {
            _cfg = cfg;
            _businessObj = businessObj;
        }

        public IActionResult GetAll()
        {
            try
            {
                List<ApplicationModuleDTO> data = _businessObj.GetAllApplicationModule();
                return Ok(new { Status = 1, Data = data });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1501, Message = "Error In Application Modules" });
        }
    }
}
