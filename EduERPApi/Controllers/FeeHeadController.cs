using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeHeadController : ControllerBase
    {
        
        UnitOfWork _unitOfWork;
        IConfiguration _cfg;

        public FeeHeadController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
            _cfg = cfg;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {

                var Res = _unitOfWork.FeeHeadMasterRepoImpl.GetById(id);
                if (Res != null)
                {
                    return Ok(new { Status = 1, Data = Res });
                }
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 904, Message = "Error In Fetching FeeHead" });
        }
        [HttpPost]
        //[Authorize(Roles = RoleConstents.SCHOOL_FEE_ADMIN_ROLE_GUID)]
        public IActionResult Add(FeeHeadMasterDTO inp)
        {
            try
            {
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                inp.OrgId = SelOrgId;
                Guid NewFeeHeadId= _unitOfWork.FeeHeadMasterRepoImpl.Add(inp);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = NewFeeHeadId });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 901, Message = "Error In Adding FeeHead" });
        }

        [HttpGet("Org/{id}")]
        public IActionResult GetAllByOrganization(Guid id)
        {
            try
            {
                var FeeHeadList = _unitOfWork.FeeHeadMasterRepoImpl.GetByParentId(id);
                
                return Ok(new { Status = 1, Data = FeeHeadList });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 902, Message = "Error In Fetching data" });

        }
    }
}
