using EduERPApi.BusinessLayer;
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

        Business _business;

        public FeeHeadController(Business business)
        {
            _business = business;
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {

                var Res = _business.GetFeeHeadById(id);

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
                (Guid newId,bool Res) Result=_business.AddFeeHeadMaster(inp);
                if(Result.Res)
                return Ok(new { Status = 1, Data = Result.newId });
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
                var FeeHeadList = _business.GetAllFeeHeadByOrganization(id);
                
                return Ok(new { Status = 1, Data = FeeHeadList });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 902, Message = "Error In Fetching data" });

        }
    }
}
