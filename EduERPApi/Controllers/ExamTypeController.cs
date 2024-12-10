using DocumentFormat.OpenXml.Office2010.Excel;
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
        UnitOfWork _unitOfWork;

        public ExamTypeController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("ByCourseId/{id}")]
        public ActionResult GetByParentId(Guid id) 
        {
            try
            {
               var Res= _unitOfWork.ExamTypeRepo.GetByParentId(id);
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
                var Res = _unitOfWork.ExamTypeRepo.Add(dto);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = "Error in Adding record" });
        }
    }

}
