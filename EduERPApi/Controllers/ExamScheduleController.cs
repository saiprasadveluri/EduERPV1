using DocumentFormat.OpenXml.Vml;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using EduERPApi.Infra;
using EduERPApi.Data;
using EduERPApi.BusinessLayer;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamScheduleController : ControllerBase
    {
        Business _business;

        public ExamScheduleController(Business business)
        {
            _business = business;
        }
        [HttpGet("File/{SchId}")]
        public IActionResult GetQuestionPaper(Guid SchId)
        {
            try
            {
               string Res= _business.GetQuestionPaper(SchId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2104, Message = "Error In Getting Question Paper" });
        }
        [HttpGet("ByExamId/{ExamId}")]
        public IActionResult GetByExamId(Guid ExamId)
        {
            try
            {

                var Res = _business.GetScheduleByExamId(ExamId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2101, Message = "Error In Getting Data" });
        }
        [HttpPost]
        public IActionResult AddSchedule([FromForm] NewExamScheduleRequestDTO dto)
        {

            bool Res=_business.AddExamSchedule(dto);
            if(Res)
            {
                return Ok(new { Status = 1, Data = Res });
            }
            return BadRequest(new { Status = 0, Data = 2101, Message = "Error In Scheduling exam" });
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteSchedule(Guid Id)
        {
            bool Res=_business.DeleteExamSchedule(Id);
            if (Res)
                return Ok(new { Status = 1, Data = Res });            
            return BadRequest(new { Status = 0, Data = 2115, Message = "Error In Deleting schedule entry" });
        }
    }
}
