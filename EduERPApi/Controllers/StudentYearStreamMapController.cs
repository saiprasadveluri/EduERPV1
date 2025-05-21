using EduERPApi.BusinessLayer;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentYearStreamMapController : ControllerBase
    {
        private Business _businessObj;

        public StudentYearStreamMapController(Business businessObj)
        {
            _businessObj = businessObj;
        }
        [HttpGet("ByCouseId/{id}")]
        public IActionResult GetMapListOnCourseId(Guid id) 
        {
            try
            {
                var Res= _businessObj.GetStudentYearStreamMapByCourseId(id);
                return Ok(new { Status = 1, Data = Res });
            }
            catch
            {

            }
            return BadRequest(new { Status = 0, Data = 1401, Message = "Error In Getting Data" });

        }
    }
}
