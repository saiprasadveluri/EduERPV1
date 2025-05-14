using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSpecializationController : ControllerBase
    {
        Business _business;
       
        public CourseSpecializationController(Business business)
        {
            _business = business;
            
        }

        [HttpGet("MainCourse/{id}")]
        public async Task<IActionResult> GetAllByParentId(Guid id)
        {
            try
            {
                var Res = _business.GetSpecialzationsByCourse(id);//_unitOfWork.CourseSpecialzationsRepo.GetByParentId(id);
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1501, Message = "Error In Fetching data" });
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecialization(SpecializationsDTO inp)
        {
            string ErrorMessage = string.Empty;
            try
            {
                bool Success = _business.AddSpecialization(inp);
                if(Success)
                    return Ok(new { Status = 1, Data = "Success" });
                
            }
            catch (Exception ex)
            {
                if(ex.InnerException is SqlException)
                {
                    ErrorMessage="Sql Data constraint violation error";
                }
                else
                    ErrorMessage = "Error In Adding data";
            }
            return BadRequest(new { Status = 0, Data = 1502, Message = ErrorMessage });
        }
        [HttpDelete("SplId")]
        public async Task<IActionResult> DeleteSpecialization(Guid SplId)
        {
            try
            {
                var Res = _business.DeleteSpecialization(SplId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1503, Message = "Error In Deleting data" });
        }
    }
}
