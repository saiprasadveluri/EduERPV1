using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectTopicController : ControllerBase
    {
        Business _business;

        public SubjectTopicController(Business business)
        {
            _business = business;
        }
        [HttpGet("{subId}")]
        public IActionResult GetAllTopicsBySubject(Guid subId)
        {
            try
            {
                var Res = _business.GetAllSubjectTopics(subId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1801, Message = "Error In Getting Subject Topics" });
        }
        [HttpPost]
        public IActionResult AddTopic(SubjectTopicDTO dto)
        {
            try
            {
               (Guid newSubjectTopicId,bool Status)= _business.AddSubjectTopic(dto);
               if(Status)
                return Ok(new { Status = 1, Data = newSubjectTopicId });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1802, Message = "Error In Adding Subject Topics" });
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
               bool Status=_business.DeleteSubjectTopic(Id);
               if(Status)
                return Ok(new { Status = 1, Data = "Success" });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1803, Message = "Error In Dleting Subject Topics" });
        }
    }
}
