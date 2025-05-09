using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcdYearController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;

        public AcdYearController(Business businessObj, IConfiguration cfg)
        {
            _businessObj = businessObj;
            _cfg = cfg;
        }

        public IActionResult GetAll()
        {
            try
            {
               List<AcdYearDTO> data= _businessObj.GetAllAcdYears();
                return Ok(new { Status = 1, Data = data });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1201, Message = "Error In Getting AcdYears" });
        }
    }
}
