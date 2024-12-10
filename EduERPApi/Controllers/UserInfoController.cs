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
        UnitOfWork _unitOfWork;
        public UserInfoController(UnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAllUsersByOrognization(Guid Id)
        {
            try
            {
                var UserList = _unitOfWork.UserInfoRepo.GetByParentId(Id);
                return Ok(new { Status = 1, Data = UserList });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 102, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserInfoDTO inp)
        {
            try
            {
                Guid NewUserId = _unitOfWork.UserInfoRepo.Add(inp);
                Guid UserOrgMapId = _unitOfWork.UserOrgMapRepoImpl.Add(
                    new UserOrgMapDTO()
                    {
                        OrgId=inp.OrgId,
                        UserId=NewUserId
                    });
                    _unitOfWork.SaveAction();
                    
                    return Ok(new { Status = 1, Data = new { NewUserId, UserOrgMapId } });
               
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 100,Message=ex.Message });
            }
        }
    }
}
