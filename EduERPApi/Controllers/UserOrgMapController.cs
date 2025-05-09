using EduERPApi.BusinessLayer;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrgMapController : ControllerBase
    {
        IConfiguration _cfg;
        private Business _businessObj;
        public UserOrgMapController(Business businessObj, IConfiguration cfg)
        {
            _businessObj = businessObj;
            _cfg = cfg;
        }

        [HttpPost]
        public async Task<IActionResult> AddMap(UserOrgMapDTO inp)
        {
            try
            {
                (Guid mapId, bool Status) = _businessObj.AddUserOrgMap(inp); //_unitOfWork.UserOrgMapRepoImpl.Add(inp);
                if(Status)
                return Ok(new { Status = 1, Data = mapId });
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 601, Message = "Error In Adding Map" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMap(UserOrgMapDTO Newinp)
        {
            try
            {
               bool Res= _businessObj.UpdateUserOrgMap(Newinp);

                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 602, Message = "Error In Updating Map" });
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMap(Guid Id)
        {
            try
            {
                bool Status=_businessObj.DeleteUserOrgMap(Id);
                if(Status)
                return Ok(new { Status = 1, Data = Status });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 602, Message = "Error In Deleting Map" });
        }
    }
}
