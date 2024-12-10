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
        UnitOfWork _unitOfWork;
       
        public CourseSpecializationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        [HttpGet("MainCourse/{id}")]
        public async Task<IActionResult> GetAllByParentId(Guid id)
        {
            try
            {
                var Res=_unitOfWork.CourseSpecialzationsRepo.GetByParentId(id);
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
                var mainCourse = _unitOfWork.MainCourseRepo.GetById(inp.MainCourseId);
                if (mainCourse != null && mainCourse.IsSpecializationsAvailable==1)
                {
                    var NewSplId = _unitOfWork.CourseSpecialzationsRepo.Add(inp);

                    for (int year = 1; year <= mainCourse.DurationInYears; ++year)
                    {
                        for (int term = 1; term <= mainCourse.NumOfTermsInYear; ++term)
                        {
                            _unitOfWork.CourseDetailRepo.Add(new CourseDetailDTO()
                            {
                                CourseDetailId = Guid.NewGuid(),
                                SpecializationId = NewSplId,
                                Year = year,
                                Term = term
                            });
                        }
                    }
                    _unitOfWork.SaveAction();
                    return Ok(new { Status = 1, Data = "Success" });
                }
                
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
        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialization(Guid splId)
        {
            try
            {
                var Res = _unitOfWork.CourseSpecialzationsRepo.Delete(splId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1503, Message = "Error In Deleting data" });
        }
    }
}
