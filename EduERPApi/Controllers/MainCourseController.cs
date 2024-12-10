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
        UnitOfWork _unitOfWork;
        public MainCourseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Org")]
        public async Task<IActionResult> GetCoursesByOrgId() 
        {
            try
            {
                var OrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                var Res = _unitOfWork.MainCourseRepo.GetByParentId(OrgId);
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
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                course.OrgId = SelOrgId;
                Guid NewCourseId = _unitOfWork.MainCourseRepo.Add(course);
                
                if (course.IsSpecializationsAvailable == 0)
                {
                    Guid NewSplId = _unitOfWork.CourseSpecialzationsRepo.Add(new SpecializationsDTO()
                    {
                        MainCourseId = NewCourseId,
                        SpecializationName = course.CourseName,
                        Status = 1
                    });
                    for (int year = 1; year <= course.DurationInYears; ++year)
                    {
                        for (int term = 1; term <= course.NumOfTermsInYear; ++term)
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
                }
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = NewCourseId });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 301, Message = ex.Message });
            }
        }
        
    }
}
