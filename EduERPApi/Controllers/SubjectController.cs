using EduERPApi.BusinessLayer;
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
        Business _business;

        public SubjectController(Business business)
        {
            _business = business;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                var Res= _business.GetAllSubjectsByOrganization(SelOrgId);
                
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
                (Guid SubId,bool Status) = _business.AddSubject(dto);
                if(Status)
                return Ok(new { Status = 1, Data = SubId });
            }
            catch (Exception ex)
            {
            }
            return BadRequest(new { Status = 0, Data = 1602, Message = "Error In Adding Data" });
        }
    }
}
