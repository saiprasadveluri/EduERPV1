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
        UnitOfWork _unitOfWork;

        public SubjectTopicController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("BySubject/{subId}")]
        public IActionResult GetAllBySubject(Guid subId)
        {
            try
            {
                var Res = _unitOfWork.SubjectTopicRepo.GetByParentId(subId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1801, Message = "Error In Getting Subject Topics" });
        }
        [HttpPost]
        public IActionResult Add(SubjectTopicDTO dto)
        {
            try
            {
               Guid newSubjectTopicId=_unitOfWork.SubjectTopicRepo.Add(dto);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = newSubjectTopicId });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1802, Message = "Error In Adding Subject Topics" });
        }
        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            try
            {
                _unitOfWork.SubjectTopicRepo.Delete(key);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = "Success" });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1803, Message = "Error In Dleting Subject Topics" });
        }
    }
}
