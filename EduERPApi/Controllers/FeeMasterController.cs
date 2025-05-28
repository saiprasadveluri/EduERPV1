using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeMasterController : ControllerBase
    {
        Business _business;

        public FeeMasterController(Business business)
        {
            _business = business;
        }

        [HttpPost]
        //[Authorize(Roles = RoleConstents.SCHOOL_FEE_ADMIN_ROLE_GUID)]
        public IActionResult Add(FeeMasterDTO inp)
        {
            try
            {
              (Guid newId,bool Res) Result=  _business.AddFeeMaster(inp);

                if(Result.Res)
                return Ok(new { Status = 1, Data = Result.newId });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1101, Message = "Error In Adding Fee" });
        }

        
    }
}
