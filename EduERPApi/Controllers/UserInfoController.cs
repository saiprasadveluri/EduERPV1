using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;
        public UserInfoController(Business businessObj, IConfiguration cfg)
        {
            _businessObj = businessObj;
            _cfg = cfg;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAllUsersByOrognization(Guid Id)
        {
            try
            {
                var UserList = _businessObj.GetAllUsersByOrognization(Id);
                return Ok(new { Status = 1, Data = UserList });
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 102, Message = "Error In operation" });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserInfoDTO inp)
        {
            try
            {
                ((Guid NewUserId,Guid UserOrgMapId), bool Status)= _businessObj.AddUserInfo(inp);
                if(Status)
                    return Ok(new { Status = 1, Data = new { NewUserId, UserOrgMapId } });
               
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 100, Message = ex.Message });
        }
    }
}
