using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCourseController : ControllerBase
    {
        Business _businessObj;
        public MainCourseController(Business businessObj)
        {
            _businessObj = businessObj;
        }
        [HttpGet("Org")]
        public async Task<IActionResult> GetCoursesByOrgId() 
        {
            try
            {
                var Res = _businessObj.GetCoursesByOrgId();                
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 302, Message = "Error In Fetching Data" });

        }
        [HttpPost]
        public async Task<IActionResult> Add(MainCourseDTO course)
        {
            try
            {
               (Guid NewCourseId, bool Status)= _businessObj.AddNewMainCourse(course);
                if(Status)
                return Ok(new { Status = 1, Data = NewCourseId });
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 301, Message = "Error In Operation" });
        }
        
    }
}
