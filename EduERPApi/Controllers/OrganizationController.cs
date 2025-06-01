using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrganizationController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;
        public OrganizationController(Business businessObj, IConfiguration cfg)
        {
            _businessObj = businessObj;
            _cfg = cfg;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Res = _businessObj.GetAllOrganizations();
                
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 201, Message = "Error In Fetching Records" });
            }
        }

        [HttpGet("{OrgId}")]
        public async Task<IActionResult> GetById(Guid OrgId)
        {
            try
            {
                var Res = _businessObj.GetOrganizationById(OrgId);
                if(Res!=null)
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 201, Message = "Error In Fetching Records" });
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrganizationDTO inp)
        {
            try
            {
                (Guid Res,bool Status) = _businessObj.AddOrganization(inp);// _unitOfWork.OrganizationRepo.Add(inp);
                if(Status)
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {
                //return BadRequest(new { Status = 0, Data = 202, Message = "Error In adding Organization" });
            }
            return BadRequest(new { Status = 0, Data = 202, Message = "Error In adding Organization" });
        }
    }
}
