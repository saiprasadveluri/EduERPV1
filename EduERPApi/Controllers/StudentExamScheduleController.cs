using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamScheduleController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        Business _business;
        public StudentExamScheduleController(Business business)
        {
            _business = business;
        }

        [HttpPost]
        public IActionResult MapExamToStudents(NewStudentExamScheduleMapRequestDTO inp)
        {
            try
            {
                bool Res = _business.MapExamToStudents(inp);
                return Ok(new { Status = Res, Data = "Success" });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2201, Message = "Error In Creating student schedule" });
        }
    }
}
