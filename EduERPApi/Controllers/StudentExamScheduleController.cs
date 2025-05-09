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

        public StudentExamScheduleController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult MapExamToStudents(NewStudentExamScheduleMapRequestDTO inp)
        {
            try
            {
                //_unitOfWork.StudentExamScheduleMapRepo.ExecuteRaw(inp.ExamId);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = "Success" });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2201, Message = "Error In Creating student schedule" });
        }
    }
}
