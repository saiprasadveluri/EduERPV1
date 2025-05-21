using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamTypeController : ControllerBase
    {
        //UnitOfWork _unitOfWork;
        Business _business;

        public ExamTypeController(Business business)
        {
            _business = business;
        }
        [HttpGet("ByCourseId/{id}")]
        public ActionResult GetByParentId(Guid id) 
        {
            try
            {
                var Res = _business.GetExamTypeByCourse(id);
                return Ok(new {Status=1,Data=Res });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new {Status=0, Data="Error in Fetching records" });
        }
        [HttpPost]
        public IActionResult Add(ExamTypeDTO dto)
        {
            try
            {
               (Guid id,bool status) Res= _business.AddExamType(dto);//_unitOfWork.ExamTypeRepo.Add(dto);
                if(Res.status)
                return Ok(new { Status = Res.status? 1:0, Data = Res.id });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = "Error in Adding record" });
        }
    }

}
