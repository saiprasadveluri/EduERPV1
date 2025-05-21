using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        //UnitOfWork _unitOfWork;
        Business _business;

        public ExamController(Business business)
        {
            _business = business;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var Res = _business.GetExamById(Id);
                return Ok(new { Success = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1201, Message = "Error In Getting AcdYears" });
        }
        [HttpGet("ByCourse/{CourseDetialId}")]
        public ActionResult GetExamByCourseId(Guid CourseDetialId) 
        {
            try
            {
                var Res= _business.GetExamByCourseId(CourseDetialId);
                return Ok(new { Success = 1, Data = Res });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Success = 0, Data = "Error in adding record" });
        }
        [HttpPost]
        public ActionResult AddExam(ExamDTO inp)
        {
            try
            {
                (Guid id,bool Status) Res = _business.AddExam(inp);
                if(Res.Status)
                return Ok(new { Success = 1, Data = Res.id });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2001, Message = "Error In Adding record" });
        }
        
    }
}
