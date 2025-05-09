using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileImportLibrary;
using EduERPApi.BusinessLayer;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;
        public StudentController(Business businessObj, IConfiguration cfg)
        {
            _businessObj = businessObj;
            _cfg = cfg;
        }

        [HttpPost("Bulk")]
        //[Authorize(Roles=RoleConstents.SCHOOL_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID)]
        public IActionResult BulkAdd([FromForm]BulkStudentInfoDTO inp)
        {
            try
            {
                bool Result = _businessObj.StudentBulkAdd(inp);
                if(Result)
                    return Ok(new { Status = 1, Data = "Success" });                        

            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 701, Message = "Error In Adding Students" });
        }

        [HttpPost]
        //[Authorize(Roles=RoleConstents.SCHOOL_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID)]
        public async Task<IActionResult> Add(StudentInfoDTO inp)
        {
            try
            {
               (Guid newUserId,bool Status)= _businessObj.AddStudent(inp);
                if(Status)
                    return Ok(new { Status = 1, Data = newUserId });                
                
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 701, Message = "Error In Adding Student"});
        }
    }
}
