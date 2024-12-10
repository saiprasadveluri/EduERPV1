using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        UnitOfWork _unitOfWork;

        public SubjectController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                var Res= _unitOfWork.SubjectRepo.GetByParentId(SelOrgId);
                
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {
            }

            return BadRequest(new { Status = 0, Data = 1601, Message = "Error In Getting Data" });
        }
        [HttpPost]
        public IActionResult Add(SubjectDTO dto)
        {
            try
            {
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                dto.OrgId = SelOrgId;
                var Res = _unitOfWork.SubjectRepo.Add(dto);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {
            }
            return BadRequest(new { Status = 0, Data = 1602, Message = "Error In Adding Data" });
        }
    }
}
